﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class SignInRequest
    {

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "User Name or Email")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        public string UserAgent { get; set; }
        public string IpAddress { get; set; }

        public string NormalizedEmail
        {
            get
            {

                return Name.ToUpper();
            }
        }
    }
}
