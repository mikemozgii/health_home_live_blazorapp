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
    [PgsDataTable(EntityAcceesNameHdr.HomeProjectTypes)]
    public class e_HomeProjectType : HHLBaseRepository<e_HomeProjectType>
    {

        [PgsPK]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsResidential { get; set; }
        public bool IsCommercial { get; set; }
        public bool IsActive { get; set; }
    }
}
