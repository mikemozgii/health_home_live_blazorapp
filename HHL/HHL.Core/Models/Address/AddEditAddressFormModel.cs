using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models.Address
{
    public class AddEditAddressFormModel
    {
        public Guid? Id { get; set; }
        public Guid? EntityId { get; set; }
        public int? FieldNameId { get; set; }
        public string Name { get; set; }
        [Required]
        public Guid? CountryId { get; set; }
        [Required]
        public Guid? RegionId { get; set; }
        [Required]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public Guid? CityId { get; set; }
        public string City { get; set; }
        public int? TypeId { get; set; }

        public int? AreaKm { get; set; }
        
    }
}
