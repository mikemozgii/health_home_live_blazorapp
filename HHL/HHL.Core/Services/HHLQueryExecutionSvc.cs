using Dapper;
using HHL.Auth.Core.Models;
using HHL.Common;
using HHL.Core.DataAccess;
using HHL.Core.DataAccess.Entities;
using HHL.Core.Handlers;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Enums;
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

namespace HHL.Core.Services
{
    public class HHLQueryExecutionSvc : QueryExecutionSvc, IHHLQueryExecutionSvc
    {
       public HHLAuthSessionSvc _AuthSessionSvc { get; set; }
       public HHLAccountSession _AccountSession { get { return _AuthSessionSvc.AccountSession; } }

        public HHLQueryExecutionSvc(HHLAuthSessionSvc authSessionSvc)
        {
            _AuthSessionSvc = authSessionSvc;
            if (authSessionSvc.IsAuthenticated)
            {
                ApplicationName = _AuthSessionSvc?.AccountSession?.ToAppName();
            }
            ConnStr = HHLConfigHdr.ConnStr;
            ProcessConnString();

        }


        public QueryResponseGeneric<T> GetByAccountIdFromSession<T>() where T : HHLBaseRepository<T>, new()
        {
            //if (vals.IsNullOrEmpty())
            //{

            //}
            return ExecuteResultQuery<T>(new T().SELECTbyColumnValue(Pairing.Of("AccountId", _AccountSession.AccountId)));
        }

        public async Task<QueryResponseGeneric<T>> GetByAccountIdFromSessionAsync<T>() where T : HHLBaseRepository<T>, new()
        {
            return await ExecuteResultQueryAsync<T>(new T().SELECTbyColumnValue(Pairing.Of("AccountId", _AccountSession.AccountId)));
        }

        public QueryFilter SetAccountIdQueryFilter(Guid accountId)
        {
            return new QueryFilter(new QueryFilter(nameof(e_Client.AccountId), QueryOperators.EQ, accountId));
        }
    }
}
