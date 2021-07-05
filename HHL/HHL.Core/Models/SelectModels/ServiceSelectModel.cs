using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class ServiceSelectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ServiceSelectModel()
        {

        }

        public ServiceSelectModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
