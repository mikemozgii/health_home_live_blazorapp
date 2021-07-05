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
    [PgsDataTable(EntityAcceesNameHdr.VmHomeTasks)]
    public class v_VmHomeTask : HHLBaseRepository<v_VmHomeTask>
    {

        [PgsPK]
        public Guid TaskId { get; set; }
        public Guid TaskHomeProjectId { get; set; }
        public string TaskName { get; set; }
        public Guid TaskClientId { get; set; }
        public Guid TaskClientAccountId { get; set; }
    
        public int TaskCategoryId { get; set; }
        public DateTime? TaskDateStart { get; set; }


        public decimal TaskEstimatedHours { get; set; }
        public decimal TaskPriceBase { get; set; }
        public decimal TaskPriceTotal { get; set; }
        public decimal TaskPriceBaseFinal { get; set; }

        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; }

        public int TaskHomeBuldingTypeId { get; set; }
        public string TaskHomeBuldingTypeName { get; set; }

        public int TaskPriorityId { get; set; }
        public string TaskPriorityName { get; set; }

    }
}
