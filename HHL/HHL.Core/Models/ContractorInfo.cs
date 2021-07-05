using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class ContractorInfo
    {
        public Guid ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string OrganizationName { get; set; }
        public string EmailName { get; set; }
        public int? ApplicationStep { get; set; }
        public int ContractorStatusId { get; set; }
        public int ContractorPlanId { get; set; }
        
    }
}
