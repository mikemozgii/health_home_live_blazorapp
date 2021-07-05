using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class AddUpdateProjectFormModel
    {
        public Guid? Id { get; set; }
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public int HomeBuldingTypeId { get; set; }
   }
}
