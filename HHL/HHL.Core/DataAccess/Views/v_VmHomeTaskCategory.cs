using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Views
{
    [PgsDataTable(EntityAcceesNameHdr.VmHomeTaskCategories)]
    public class v_VmHomeTaskCategory : HHLBaseRepository<v_VmHomeTaskCategory>
    {

        [PgsPK]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        
        public int HomeBuldingTypeId { get; set; }
        public string HomeBuldingTypeName { get; set; }

        public int HomeTaskTypeId { get; set; }
        public string HomeTaskTypeName { get; set; }

        public int HomeTaskServiceTypeId { get; set; }
        public string HomeTaskServiceTypeName { get; set; }
        public string HomeTaskServiceTypeIcon { get; set; }

    }
}
