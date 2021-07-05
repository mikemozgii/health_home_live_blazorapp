using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HHL.Core.Models
{
    public class AddFileModel
    {
        public string Name { get; set; }
        public byte[] Stream { get; set; }
        public int? FieldNameId { get; set; }
        public Guid EntityId { get; set; }
    }
}
