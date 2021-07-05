using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Models
{
    public class ProductTypeSelectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ProductTypeSelectModel()
        {

        }

        public ProductTypeSelectModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
