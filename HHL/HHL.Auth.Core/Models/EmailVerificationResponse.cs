using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class EmailVerificationResponse
    {
        public bool Success { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<ErrorMdl> Errors { get; set; }

        public EmailVerificationResponse(bool _success = false)
        {

            Success = _success;
        }

        public EmailVerificationResponse(IEnumerable<ErrorMdl> _errors)
        {
            Errors = _errors;
        }


        public EmailVerificationResponse(bool _success, params ErrorMdl[] _errors)
        {
            Success = _success;
            Errors = _errors;
        }
    }
}
