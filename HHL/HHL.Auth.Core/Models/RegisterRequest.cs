using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class RegisterRequest
    {

        public string AccountName { get; set; }
  
        public string Email { get; set; }
        public string NormalizedEmail { get {

                return Email.ToUpper();
            } }

        public string Password { get; set; }
    }
}
