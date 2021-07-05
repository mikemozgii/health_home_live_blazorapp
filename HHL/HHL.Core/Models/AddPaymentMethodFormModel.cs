using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{
    public class AddPaymentMethodFormModel
    {
        [Required]
        public long? CardNumber { get; set; }
        [Required]
        public int? ExpiryYear { get; set; }
        [Required]
        public int? ExpiryMonth { get; set; }
        [Required]
        public int? Cvc { get; set; }
        [Required]
        public string CardholderName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode  { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
