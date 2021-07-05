using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{

    public class EditContractorInfoModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIdentity { get; set; }
        public string SIN { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public Guid? PrimaryLanguageId { get; set; }
        public Guid? SecondaryLanguageId { get; set; }

        public Guid? PrimaryPhoneId { get; set; }
        public Guid? SecondaryPhoneId { get; set; }



        public Guid? PrimaryEmailId { get; set; }

        [EmailAddress]
        public string PrimaryEmailName { get; set; }


        public Guid? SecondaryEmailId { get; set; }

        [EmailAddress]
        public string SecondaryEmailName { get; set; }

        public Guid? BillingAddressId { get; set; }

        public bool HasLocation { get; set; }

        public Guid? LocationAddressId { get; set; }

    }
}
