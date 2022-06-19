﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MainServer.Attributes;
using MainServer.Captcha;
using MainServer.DataBase;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;

namespace MainServer.Token
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly DB db;
        private readonly IConfiguration config;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration, DB database)
        {
            _logger = logger;
            config = configuration;
            db = database;
        }

        [HttpPost("Authorization")]
        public IActionResult Authorization([FromBody] AuthModel authModel)//Авторизация
        {
            var user = db.Users.FirstOrDefault(x => x.Login == authModel.Login);
            if (string.IsNullOrEmpty(authModel.Password) || authModel.Password?.ToString() == "null")
                return Ok(GetJwt(new UserDB { Login = authModel.Login, Role = "Anonymous" }));

            if (user == null || user.Password != authModel.Password)
                return Unauthorized("Invalid login or password");

            if (user != null && user.Password == authModel.Password)
                return Ok(GetJwt(user));

            return BadRequest("Account not found");
        }
        //return new StatusCodeResult(1);//(int)response.StatusCode==1
        [HttpPost("Registration")]
        public IActionResult Registration([FromBody] RegModel RegModel)
        {
            string ServerCaptcha = HttpContext.Session.GetString("code");
            //SessionClass.Session.TryGetValue(RegModel.Guid, out ServerCaptcha);

            if (RegModel.Captcha != ServerCaptcha)
                return BadRequest("Captcha was not correct");

            //SessionClass.Session.Remove(RegModel.Guid);

            var user = db.Users.FirstOrDefault(a => a.Login.ToLower().ToLower() == RegModel.Login.Trim().ToLower());
            if (user != null) return Conflict(($"User name " + RegModel.Login + " is already taken"));

            var questions = db.SecurityQuestions.ToList().FirstOrDefault(x => x.id == RegModel.QuestionsId);
            if (questions == null) return BadRequest("Secret question not found");

            var NewUser = db.Users.Add(new UserDB
            {
                Login = RegModel.Login.Trim(),
                Password = RegModel.Password,
                Role = "User",
                SecurityQuestionId = questions.id,
                AnswerSecurityQ = RegModel.AnswersecurityQ
            }).Entity;

            db.SaveChanges();

            return Ok(GetJwt(NewUser));
        }
        [HttpPost("СhangePasswordbySecurityQuestions")]
        public IActionResult СhangePasswordbySecurityQuestions([FromBody] ChangPassModel ChangModel)
        {
            var user = db.Users.SingleOrDefault(a => a.Login.ToLower() == ChangModel.Login.Trim().ToLower());
            if (user == null) return BadRequest("Account not found"); ;

            if (user.SecurityQuestionId == ChangModel.QuestionsId && user.AnswerSecurityQ.ToLower() == ChangModel.AnswersecurityQ.ToLower())
            {
                user.Password = ChangModel.newPassword;
                db.SaveChanges();
                return Ok("Your password has been successfully changed");
            }
            return Unauthorized("Wrong answer");
        }

        [HttpPost("AddRole")]
        public IActionResult AddRole([FromBody] AddRole addRole)//Авторизация
        {
            var opUser = db.Users.FirstOrDefault(x => x.Login == addRole.OpLogin);

            if (opUser == null) return BadRequest("Пользователь с данным именем не найден");

            var admin = db.Users.FirstOrDefault(x => x.Login == addRole.Login);

            if (admin == null || admin.Password != addRole.Password || admin.Role != "Admin")
                return Unauthorized("Неверный логин или пароль");

            if (addRole.OpLogin != "Admin" && (addRole.Role == "User" || addRole.Role == "Moderator"))
            {
                opUser.Role = addRole.Role;
                db.SaveChanges();
                return Ok("Роль успешно установлена");
            }
            return BadRequest("Роль может быть только User или Moderator");
        }
        [HttpGet("SecurityQuestions")]
        public IActionResult SecurityQuestions()
        {
            return Ok(db.SecurityQuestions.Select(x => new { id = x.id, Questions = x.Questions }).ToList());
        }
        private IEnumerable<Claim> GetClaims(UserDB User)
        {
            var claims = new List<Claim>
            {
            new Claim("Name", User.Login),
            new Claim(ClaimTypes.Role,User.Role)
            };
            return claims;
        }
        #region Сессия
        // HttpContext.Session.SetString(Guid.NewGuid().ToString(), code);
        // HttpContext.Session.SetString(guid.ToString(), code);
        #endregion
        [HttpPost("CaptchaGenerator")]
        public ActionResult Captcha([FromBody] string guid)
        {
            if (string.IsNullOrEmpty(guid) || guid?.ToString() == "null") return BadRequest();
            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();

            HttpContext.Session.SetString("code", code);

            //SessionClass.Session[guid.ToString()] = code;

            CaptchaImage captcha = new CaptchaImage(code, 60, 30);
            this.Response.Clear();
            Image image = captcha.Image;
            byte[] img_byte_arr = ImageMethod.ImageToBytes(image);
            ImagePacket packet = new ImagePacket(img_byte_arr);
            var json = JsonSerializer.Serialize<ImagePacket>(packet);
            return Ok(json);
        }
        private string GetJwt(UserDB User)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: GetClaims(User),
                expires: now.AddMinutes(AuthOptions.Lifetime),
                signingCredentials: new SigningCredentials(AuthOptions.PrivateKey, SecurityAlgorithms.RsaSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            return tokenString;
        }
    }
}
