using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class EditAddressModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? RegionId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
    }
}
