using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class AddSheduleDayModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<AddSheduleShiftModel> AddSheduleDayModels_Include { get; set; }
    }
}
