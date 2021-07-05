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
    [PgsDataTable(EntityAcceesNameHdr.EditContractorInfoes)]
    public class v_EditContractorInfo : HHLBaseRepository<v_EditContractorInfo>
    {

        [PgsPK]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIdentity { get; set; }
        public string OrganizationTaxNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

 

        public Guid? PrimaryEmailId { get; set; }
        public string PrimaryEmailName { get; set; }


        public Guid? PrimaryPhoneId { get; set; }
        public Guid? PhoneCountryCodeId { get; set; }
        public string PhoneNumber { get; set; }

        public Guid? PrimaryAddressId { get; set; }
       
        public Guid? PrimaryAddressCityId { get; set; }
      
        public Guid? PrimaryAddressCountryId { get; set; }
     
        public Guid? PrimaryAddressRegionId { get; set; }
     
        public string PrimaryAddressLine1 { get; set; }

        public string PrimaryAddressLine2 { get; set; }
    
        public string PrimaryAddressPostalCode { get; set; }


    }
}