using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class AreaKmSelectModel
    {
        public int Value { get; set; }
        public string Name { get; set; }


        public AreaKmSelectModel()
        {

        }

        public AreaKmSelectModel(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
