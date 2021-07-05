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
    [PgsDataTable(EntityAcceesNameHdr.HomeProjects)]
    public class e_HomeProject : HHLBaseRepository<e_HomeProject>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public int HomeTaskStatusId { get; set; }
        public int PriorityId { get; set; }
        public int HomeBuldingTypeId { get; set; }

        
        //public DateTime DateCreated { get; set; }
        //public Guid CreatedBy { get; set; }
        //public DateTime DateModified { get; set; }
        //public Guid ModifiedBy { get; set; }
        //public DateTime? DateDeleted { get; set; }
        //public Guid? DeletedBy { get; set; }
    }
}




