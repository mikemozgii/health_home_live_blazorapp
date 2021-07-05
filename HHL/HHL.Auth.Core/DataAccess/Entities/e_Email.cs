using HHL.Auth.Core.Handlers;
using HHL.Auth.Core.Models;
using HHL.Auth.Core.Services;
using HHL.Common;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Handlers;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using HHL.PostreSQL.Core.Uuid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Auth.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesName.Emails)]
    public class e_Email : AuthBaseRepository<e_Email>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateVerified { get; set; }
        public string VerificationCode { get; set; }


        public QueryRequest INSERT(RegisterRequest model, Guid emailId)
        {
            var query = new QueryBuilderHandler().INSERT(EntityAcceesName.Emails, Pairing.Of(nameof(Id), emailId), Pairing.Of(nameof(Name), model.NormalizedEmail));
            return new QueryRequest(query);
        }


        public QueryRequest UPDATE(string email, string code)
        {
            return new UpdateQuery<e_Email>(QueryFilter.Equal(nameof(Name), email), Pairing.Of(nameof(VerificationCode), code)).Request;
        }

        public QueryRequest UPDATE(string email, string verificationCode, DateTime dateVerified)
        {
            return  new UpdateQuery<e_Email>(new QueryFilter(new QueryFilter(nameof(Name), QueryOperators.EQ, email), new QueryFilter(nameof(VerificationCode), QueryOperators.EQ, verificationCode)), new ReturnDataSettings(), Pairing.Of(nameof(DateVerified), dateVerified)).Request;
        }

    }
}
