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
    [PgsDataTable(EntityAcceesNameHdr.Files)]
    public class e_File : HHLBaseRepository<e_File>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileTypeId { get; set; }
        public long Size { get; set; }
        public int? FieldNameId { get; set; }
        public Guid EntityId { get; set; }
    }
}
