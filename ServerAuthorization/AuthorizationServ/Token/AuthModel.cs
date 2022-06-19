﻿using System.ComponentModel.DataAnnotations;

namespace AuthorizationServ.Token
{
    public class AuthModel
    {
        [Required(ErrorMessage = "Login can be from 6 to 15 characters")]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string Login { get; set; }

        [StringLength(200, MinimumLength = 0, ErrorMessage = "Password can be from 6 to 15 characters")]
        private string _password;
        public string Password
        {
            get
            {
                return _password.GetSha1();
            }
            set
            {
                _password = value;
            }
        }

    }

    public class RegModel
    {
        [Required(ErrorMessage = "Login can be from 6 to 15 characters")]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password can be from 6 to 15 characters")]
        [StringLength(200, MinimumLength = 6)]
        private string _password;
        public string Password
        {
            get
            {
                return _password.GetSha1();
            }
            set
            {
                _password = value;
            }
        }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10)]
        public string Guid { get; set; }

        [Required(ErrorMessage = "Captcha can be from 4 to 5 characters")]
        [StringLength(maximumLength: 5, MinimumLength = 4)]
        public string Captcha { get; set; }

        [Required(ErrorMessage = "Enter the answer to the security question")]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string AnswersecurityQ { get; set; }

        [Required(ErrorMessage = "Select security question")]
        [Range(0, 15)]
        public int QuestionsId { get; set; }
    }

    public class ChangPassModel
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password can be from 6 to 15 characters")]
        [StringLength(200, MinimumLength = 6)]
        private string _password;
        public string newPassword
        {
            get
            {
                return _password.GetSha1();
            }
            set
            {
                _password = value;
            }
        }

        [Required(ErrorMessage = "Select security question")]
        [Range(1, 10)]
        public int QuestionsId { get; set; }

        [Required(ErrorMessage = "Enter the answer to the security question")]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string AnswersecurityQ { get; set; }
    }

    public class AddRole
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string Login { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 6)]
        private string _password;
        public string Password
        {
            get
            {
                return _password.GetSha1();
            }
            set
            {
                _password = value;
            }
        }
        [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Enter role")]
        public string Role { get; set; }
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string OpLogin { get; set; }
    }

    public class ServerInfo
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 6)]
        private string _password;
        public string Password
        {
            get
            {
                return _password.GetSha1();
            }
            set
            {
                _password = value;
            }
        }
        [StringLength(maximumLength: 30, MinimumLength = 0, ErrorMessage = "Title can be maximum 16 characters")]
        public string Title { get; set; }
        [StringLength(maximumLength: 16, MinimumLength = 0, ErrorMessage = "Description can be maximum 16 characters")]
        public string Country { get; set; }
        [StringLength(maximumLength: 70, MinimumLength = 0, ErrorMessage = "Country can be maximum 70 characters")]
        public string City { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 0, ErrorMessage = "City can be maximum 25 characters")]
        public string Ip { get; set; }
        [StringLength(maximumLength: 16, MinimumLength = 0,ErrorMessage = "Description can be maximum 16 characters")] 
        public string Description { get; set; }
    }

    public class Settings
    {
        public string urls { get; set; }
        public string IPchat { get; set; }
    }
}