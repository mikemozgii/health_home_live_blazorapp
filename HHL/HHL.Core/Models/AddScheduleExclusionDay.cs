using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class AddScheduleExclusionDay
    {
        public string Name { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsStatHoliday { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}
