using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HHL.Core.Models.Client
{
    public class Client_EditSettingsFormModel
    {
        [Required]
        public Guid TimeZoneId { get; set; }

        public Guid? DefaultCountryId { get; set; }
        public Guid? DefaultRegionId { get; set; }
        public Guid? DefaultCityId { get; set; }
    }
}
