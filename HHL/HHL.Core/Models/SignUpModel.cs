using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "The field is required")]
        [DisplayName("Username")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Username { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [DisplayName("Confirm Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password does not match the confirm password.")]
        public string ConfirmPassword { get; set; }
    }
}
