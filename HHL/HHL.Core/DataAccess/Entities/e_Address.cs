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
    [PgsDataTable(EntityAcceesNameHdr.Addresses)]
    public class e_Address : HHLBaseRepository<e_Address>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }       
        public string City { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? RegionId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public Guid? EntityId { get; set; }
        public int? FieldNameId { get; set; }
        public int? TypeId { get; set; }
        public Guid? CityId { get; set; }
        public int? AreaKm { get; set; }
        
    }
}