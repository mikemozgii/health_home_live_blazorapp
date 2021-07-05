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
    [PgsDataTable(EntityAcceesNameHdr.ContractorExcludeSchedules)]
    public class e_ContractorExcludeSchedule : HHLBaseRepository<e_ContractorExcludeSchedule>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid ContractorId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsAllDay { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }


    }
}