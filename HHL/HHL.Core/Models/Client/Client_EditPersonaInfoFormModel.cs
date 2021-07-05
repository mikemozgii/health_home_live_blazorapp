using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{


    public class Client_EditPersonaInfoFormModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string OrganizationName { get; set; }

        [Required]
        public DateTime?  DateOfBirth { get; set; }

        [Required]
        public Guid PrimaryLanguageId { get; set; }
        public Guid? SecondaryLanguageId { get; set; }


    }
}
