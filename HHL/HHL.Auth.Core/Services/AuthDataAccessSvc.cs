using HHL.Auth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Services
{
    public class AuthDataAccessSvc : IAuthDataAccessWorker
    {
        public IAuthQueryExecutionSvc _AuthQueryExecutionSvc { get; set; }

        public AuthDataAccessSvc(IAuthQueryExecutionSvc queryExecutionSvc)
        {
            _AuthQueryExecutionSvc = queryExecutionSvc;
        }
    }
}
