using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{
    public class AddNewHomeTaskModel
    {
        //public int TaskServiceId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool UseClientPrimaryAddress { get; set; }
        public string CustomField1 { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        public decimal PriceBase { get; set; }
        public int HomeTaskCategoryId { get; set; }

        public decimal PriceMultiplier { get; set; }
        [Required]
        public decimal EstimatedHours { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal PriceBaseFinal { get; set; }
        public bool UseClientPrimaryContactInfo { get; set; }
        public int PriorityId { get; set; }

        // public EditAddressModel EditAddressModel { get; set; }

        //address inner classes is not supported
        public Guid? AddressId { get; set; }

        //[Required]
        //public Guid CityId { get; set; }
        //[Required]
        //public Guid? CountryId { get; set; }
        //[Required]
        //public Guid? RegionId { get; set; }
        //public string Line1 { get; set; }
        //[Required]
        //public string Line2 { get; set; }
        //[Required]
        //public string PostalCode { get; set; }

        public Guid? ContactCountryCodeId { get; set; }
        public string ContactPhoneNumber { get; set; }
   
        public string ContactEmail { get; set; }
    }
}
