using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<ErrorMdl> Errors { get; set; }

        public RegisterResponse(bool _success = false)
        {

            Success = _success;
        }

        public RegisterResponse(IEnumerable<ErrorMdl> _errors)
        {
            Errors = _errors;
        }
    }
}
