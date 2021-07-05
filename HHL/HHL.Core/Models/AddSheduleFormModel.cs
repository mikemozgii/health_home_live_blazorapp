using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class AddSheduleFormModel
    {
        public List<AddSheduleDayModel> Days { get; set; }

        public List<AddScheduleExclusionDay> ExclusionDays { get; set; }
}
}
