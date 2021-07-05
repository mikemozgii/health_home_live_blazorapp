using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models.Contractor
{
    public class EditContractorLegalInfoFormModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
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

        [EmailAddress]
        [Required]
        public string PrimaryEmailName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public Guid? PhoneCountryCodeId { get; set; }

        //public string SIN { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public Guid? PrimaryAddressCityId { get; set; }
        [Required]
        public Guid? PrimaryAddressCountryId { get; set; }
        [Required]
        public Guid? PrimaryAddressRegionId { get; set; }
        [Required]
        public string PrimaryAddressLine1 { get; set; }

        public string PrimaryAddressLine2 { get; set; }
        [Required]
        public string PrimaryAddressPostalCode { get; set; }
    }
}
