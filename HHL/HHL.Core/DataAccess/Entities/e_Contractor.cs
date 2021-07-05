using HHL.Auth.Core.Handlers;
using HHL.Core.Handlers;
using HHL.Core.Services;
using HHL.PostreSQL.Core;
using HHL.PostreSQL.Core.Attributes;
using HHL.PostreSQL.Core.Enums;
using HHL.PostreSQL.Core.Models;
using HHL.PostreSQL.Core.Services;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.DataAccess.Entities
{
    [PgsDataTable(EntityAcceesNameHdr.Contractors)]
    public class e_Contractor : HHLBaseRepository<e_Contractor>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string OrganizationName { get; set; }
        public string OrganizationIdentity { get; set; }
        public string OrganizationTaxNumber { get; set; }
        
        //public string SIN { get; set; }

        public Guid? PrimaryEmailId { get; set; }
        //public Guid? SecondaryEmailId { get; set; }

        public Guid? PrimaryPhoneId { get; set; }




        public Guid? PrimaryAddressId { get; set; }

        public int? ApplicationStep { get; set; }
        public int? ApplicationStatus { get; set; }
        public string Skype { get; set; }

        public int ContractorPlanId { get; set; }
        public int ContractorStatusId { get; set; }

        public Guid? IdentityFileId { get; set; }
        public Guid? WorkAuthorizationFileId { get; set; }
        public Guid? PhotoFileId { get; set; }




        public int? CompanyTypeId { get; set; }
        public int? CompanySizeId { get; set; }
        public int? IndustryId { get; set; }
        public int? YearFounded { get; set; }
        public string OrganizationAbout { get; set; }
        public string OrganizationTagline { get; set; }
        public bool ShareProfileWithPublic { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string WebSite { get; set; }

        public Guid? Language1Id { get; set; }
    public Guid? Language2Id { get; set; }
public Guid? Language3Id { get; set; }


        public string StripeCustomerId { get; set; }

    }
}
