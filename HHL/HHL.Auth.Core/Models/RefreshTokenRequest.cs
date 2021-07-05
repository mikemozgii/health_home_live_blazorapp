using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class RefreshTokenRequest
    {
        public string OriginalToken { get; set; }
    }
}
