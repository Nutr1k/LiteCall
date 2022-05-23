﻿using System.ComponentModel.DataAnnotations;

namespace AuthorizationServ.Token
{
    public class AuthModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        public string Login { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Password { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Guid { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 1)]
        public string Captcha { get; set; }
    }
}