using HHL.Auth.Core.Handlers;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesNameHdr.Contractors_Services)]
    public class e_Contractor_Service : HHLBaseRepository<e_Contractor_Service>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid ContractorId { get; set; }
        public int ServiceId { get; set; }
        public decimal? PricePerHour { get; set; }
        public bool IsCustomPrice { get; set; }
        public int ProductTypeId { get; set; }

        
    }
}
