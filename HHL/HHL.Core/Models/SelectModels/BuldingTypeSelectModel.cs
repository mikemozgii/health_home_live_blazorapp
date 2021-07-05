using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class BuldingTypeSelectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public BuldingTypeSelectModel()
        {

        }

        public BuldingTypeSelectModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
