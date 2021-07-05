using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class SignInResponse
    {
        public bool Success { get; set; }
        public AccountSession AccountSession { get; set; }
        public string Token { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<ErrorMdl> Errors { get; set; }

        public SignInResponse(bool _success = false)
        {

            Success = _success;
        }

        public SignInResponse(IEnumerable<ErrorMdl> _errors)
        {
            Errors = _errors;
        }
    }
}
