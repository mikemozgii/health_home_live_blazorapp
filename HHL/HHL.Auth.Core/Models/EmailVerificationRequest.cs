using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class EmailVerificationRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail
        {
            get
            {

                return Email.ToUpper();
            }
        }
        public bool IsValid => !string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(Email);

    }
}
