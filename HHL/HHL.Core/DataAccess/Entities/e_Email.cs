using HHL.Auth.Core.Handlers;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesNameHdr.Emails)]
    public class e_Email : HHLBaseRepository<e_Email>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateVerified { get; set; }
        public string VerificationCode { get; set; }
    }
}