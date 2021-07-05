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
    [PgsDataTable(EntityAcceesNameHdr.HomeTaskCategories)]
    public class e_HomeTaskCategory : HHLBaseRepository<e_HomeTaskCategory>
    {

        [PgsPK]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HomeBuldingTypeId { get; set; }
        public int HomeTaskTypeId { get; set; }
        public int HomeTaskServiceTypeId { get; set; }
        public bool IsActive { get; set; }
        public string FormSettings { get; set; }

        public decimal PriceMultiplier { get; set; }
        public decimal MinHours { get; set; }
    }
   
}
