using HHL.Auth.Core.DataAccess.Entities;
using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Auth.Core.DataAccess.Views
{
    [PgsDataTable(EntityAcceesName.AccountAuths)]
    public class v_AccountAuth : AuthBaseRepository<v_AccountAuth>
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public Guid EmailId { get; set; }
        public string EmailName { get; set; }
        public bool IsEmailVerified { get; set; }

        public int AccountAccessFailedCount { get; set; }
        public DateTime? AccountAccessFailedDate { get; set; }
        public int AccountAccessSuccessCount { get; set; }
        public DateTime? AccountAccessSuccessDate { get; set; }


        public QueryRequest SELECT(string accountName)
        {
            return new v_AccountAuth().SELECTbyColumnValue(Pairing.Of(nameof(AccountName), accountName));
        }

        public QueryRequest SELECT(string accountName, string emailName)
        {
            var f = new QueryFilter(QueryLogicalOperators.OR, new QueryFilter(nameof(AccountName), QueryOperators.EQ, accountName), new QueryFilter(nameof(EmailName), QueryOperators.EQ, emailName.ToUpper()));
            return new SelectQueryGeneric<v_AccountAuth>(f).Request;
        }

    }
}
