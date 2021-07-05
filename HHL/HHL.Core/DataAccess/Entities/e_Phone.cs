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
    [PgsDataTable(EntityAcceesNameHdr.Phones)]
    public class e_Phone : HHLBaseRepository<e_Phone>
    {
        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public Guid? CountryCodeId { get; set; }
    }
}