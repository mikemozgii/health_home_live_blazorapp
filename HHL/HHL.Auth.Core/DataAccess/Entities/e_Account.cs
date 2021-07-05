using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using HHL.PostreSQL.Core.Uuid;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesName.Accounts)]
    public class e_Account: AuthBaseRepository<e_Account>
    {
        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid? EmailId { get; set; }
        public string Token { get; set; }
        public DateTime? DateLastSignIn { get; set; }
        public int AccessFailedCount { get; set; }
        public int AccessSuccessCount { get; set; }
        public DateTime? AccessFailedDate { get; set; }
        public DateTime? AccessSuccessDate { get; set; }

        public QueryRequest INSERT(RegisterRequest model, Guid emailId, Guid accontId)
        {
            var query = new QueryBuilderHandler().INSERT(EntityAcceesName.Accounts, 
                Pairing.Of(nameof(Id), accontId), 
                Pairing.Of(nameof(Name), model.AccountName), 
                Pairing.Of(nameof(Password), new BCryptSvc().Hash(model.Password, AuthConfigHdr.BcryptAccountPasswordSalt)), 
                Pairing.Of(nameof(EmailId), emailId));

            return new QueryRequest($"{query}");
        }


    }
}
