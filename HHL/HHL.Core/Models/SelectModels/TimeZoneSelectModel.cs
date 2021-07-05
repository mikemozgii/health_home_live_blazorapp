using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class TimeZoneSelectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double GMTOffset { get; set; }
        public double DSTOffset { get; set; }
        public double RAWOffset { get; set; }
        public Guid? CountryId { get; set; }
    }
}
