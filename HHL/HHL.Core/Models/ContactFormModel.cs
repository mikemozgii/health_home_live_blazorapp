using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "The field is required")]
        [StringLength(maximumLength: 100, MinimumLength= 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field is required")]
        [EmailAddress]
        [StringLength(maximumLength: 100, MinimumLength = 6)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field is required")]
        [StringLength(maximumLength: 100, MinimumLength = 6)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "The field is required")]
        [StringLength(maximumLength: 250, MinimumLength = 6)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [StringLength(maximumLength: 500, MinimumLength = 6)]
        public string Message { get; set; }
        
    }
}
