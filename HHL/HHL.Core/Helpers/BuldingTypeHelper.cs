using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Core.Helpers
{
    public  class BuldingTypeHelper
    {
        public string GetName(int id)
        {

            switch (id)
            {
                case 1:
                    return "residential";
                case 2:
                    return "commercial";
                default:
                    return "Unknown";
            }
        }

        public int GetId(string name)
        {
            switch (name)
            {
                case "residential":
                    return 1;
                case "commercial":
                    return 2;
                default:
                    return -1;
            }
        }

    }
}
