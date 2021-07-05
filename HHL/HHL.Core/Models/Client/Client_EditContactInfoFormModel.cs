using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{


    public class Client_EditContactInfoFormModel
    {
        [Required]
        public Guid? PrimaryPhoneId { get; set; }

        [Required]
        public Guid? PrimaryPhoneCountryCodeId { get; set; }
        [Required]
        public string PrimaryPhoneNumber{ get; set; }

        [Required]
        public Guid PrimaryEmailId { get; set; }
        [Required]
        public string PrimaryEmailName { get; set; }
    }
}
