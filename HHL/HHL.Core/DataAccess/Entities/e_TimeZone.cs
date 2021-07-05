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
    [PgsDataTable(EntityAcceesNameHdr.TimeZones)]
    public class e_TimeZone : HHLBaseRepository<e_TimeZone>
    {
        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double GMTOffset { get; set; }
        public double DSTOffset { get; set; }
        public double RAWOffset { get; set; }
        public Guid? CountryId { get; set; }
    }
}