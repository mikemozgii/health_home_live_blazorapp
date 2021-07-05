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
    [PgsDataTable(EntityAcceesNameHdr.VmHomeProjects)]
    public class v_VmHomeProject : HHLBaseRepository<v_VmHomeProject>
    {

        [PgsPK]
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }

        public int ProjectStatusId { get; set; }
        public string ProjectStatusName { get; set; }

        public Guid ProjectClientId { get; set; }
        public Guid ProjectAccountId { get; set; }

        public int ProjectPriorityId { get; set; }
        public string ProjectPriorityName { get; set; }

        public int ProjectHomeBuldingTypeId { get; set; }
        public string ProjectHomeBuldingTypeName { get; set; }

        public int TaskCount { get; set; }
        public decimal? TaskPriceTotal { get; set; }
    }
}
