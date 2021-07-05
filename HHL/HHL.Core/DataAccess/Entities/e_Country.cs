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
    [PgsDataTable(EntityAcceesNameHdr.Countries)]
    public class e_Country : HHLBaseRepository<e_Country>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ISOAlpha3 { get; set; }
        public int ISONumeric { get; set; }
        public Guid? ContinentId { get; set; }
        public int PhoneCode { get; set; }
    }
}
