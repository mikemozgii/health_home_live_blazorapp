using HHL.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Models
{
    public class AccountSession
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string EmailName { get; set; }
        public string Token { get; set; }

        public string ToAppName()
        {
            return AccountId.ToAppName();
        }
    }
}
