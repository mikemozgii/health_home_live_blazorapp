using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class EditPhoneNumberModel
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }
        public Guid? CountryCodeId { get; set; }
    }
}
