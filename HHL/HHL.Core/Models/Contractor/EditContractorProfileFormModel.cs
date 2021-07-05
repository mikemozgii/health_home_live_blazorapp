using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models.Contractor
{
    public class EditContractorProfileFormModel
    {
        public string WebSite { get; set; }

        public int? IndustryId { get; set; }
        public int? CompanyTypeId { get; set; }
        public int? CompanySizeId { get; set; }
        public int? YearFounded { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string OrganizationAbout { get; set; }
        public string OrganizationTagline { get; set; }
        public string Skype { get; set; }
        
        public bool ShareProfileWithPublic { get; set; }

        public Guid? Language1Id { get; set; }
        public Guid? Language2Id { get; set; }
        public Guid? Language3Id { get; set; }
    }
}
