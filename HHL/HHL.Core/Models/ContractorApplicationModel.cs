using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{

    public class ContractorApplicationModel
    {

        public Guid? Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string OrganizationName { get; set; }
        [Required]
        public string OrganizationIdentity { get; set; }
        [Required]
        public string OrganizationTaxNumber { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [EmailAddress]
        [Required]
        public string PrimaryEmailName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid? PhoneCountryCodeId { get; set; }
        public Guid? PrimaryPhoneId { get; set; }
        public Guid? PrimaryAddressId { get; set; }
        public Guid? PrimaryEmailId { get; set; }
        //[Required]
        //public string Skype { get; set; }

        [Required]
        public Guid? CityId { get; set; }
        [Required]
        public Guid? CountryId { get; set; }
        [Required]
        public Guid? RegionId { get; set; }
        [Required]
        public string Line1 { get; set; }
     
        public string Line2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
