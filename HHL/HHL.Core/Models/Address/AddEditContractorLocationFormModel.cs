using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models.Address
{
    public class AddEditContractorLocationFormModel
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
        public Guid? CityId { get; set; }
        public int? TypeId { get; set; }

        public int? AreaKm { get; set; }
        
    }
}
