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
    [PgsDataTable(EntityAcceesNameHdr.HomeTasks)]
    public class e_HomeTask : HHLBaseRepository<e_HomeTask>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HomeTaskCategoryId { get; set; }
        public int HomeTaskStatusId { get; set; }
        public Guid HomeProjectId { get; set; }

        public decimal PriceBase { get; set; }
        public decimal PriceMultiplier { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal PriceBaseFinal { get; set; }
        public DateTime? DateStart{ get; set; }

        public string Brand { get; set; }
        public Guid AddressId { get; set; }
        public string Model { get; set; }
        public Guid? ContactCountryCodeId { get; set; }       
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }

        public bool UseClientPrimaryContactInfo { get; set; }
        
        public Guid SelectedAddressId { get; set; }

        public int PriorityId { get; set; }


    }
}




