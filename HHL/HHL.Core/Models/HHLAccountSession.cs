using HHL.Auth.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class HHLAccountSession : AccountSession
    {

        public ContractorInfo ContractorInfo { get; set; }
        public ClientInfo ClientInfo { get; set; }
    }
}
