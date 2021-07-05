using Dapper;
using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Interfaces;
using HHL.Common;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Auth.Core.Services
{
    public class AuthQueryExecutionSvc : QueryExecutionSvc, IAuthQueryExecutionSvc
    {
        public AuthQueryExecutionSvc()
        {
            ConnStr = AuthConfigHdr.ConnStr;
            ApplicationName = "AuthServer";
            ProcessConnString();
        }

        public AuthQueryExecutionSvc(string connStr, string applicationName = null) : base(connStr, applicationName)
        {

        }
    }
}
