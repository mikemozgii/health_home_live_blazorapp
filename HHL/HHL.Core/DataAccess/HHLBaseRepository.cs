using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Common;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess
{
    public class HHLBaseRepository<T> : BaseRepository<T> where T : class
    {
        public HHLBaseRepository()
        {
        }


        public QueryRequest GetByAccountId(AccountSession accountSession)
        {
            if (accountSession != null)
            {
                return GetByAccountId(accountSession.AccountId);
            }
            return null;
        }

        public QueryRequest GetByAccountId(Guid accountId)
        {
            return SELECTbyColumnValue(Pairing.Of("AccountId", accountId));
        }
    }

    //public SessionSvc SessionSvc { get; set; }
    //public HHLBaseRepository()
    //    {
    //        EntityConnStr = HHLConfigHdr.ConnStr;
    //    }

        //public HHLBaseRepository(SessionSvc SessionSvc)
        //{
           
        //    EntityConnStr = HHLConfigHdr.ConnStr;
        //    if (SessionSvc?.AccountSession != null)
        //    {
        //        SessionSvc = SessionSvc;
        //        EntityConnStr = $"{HHLConfigHdr.ConnStr};ApplicationName={SessionSvc.AccountSession.AccountId}";
        //    }
        //}

    //}
}
