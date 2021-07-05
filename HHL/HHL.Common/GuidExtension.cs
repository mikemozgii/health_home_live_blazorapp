using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHL.Common
{
    public static class GuidExtension
    {
        public static string ToAppName(this Guid input)
        {
            if (input == null) return null;

            return $"accId:{input}";
        }
        //public static double? GetDoubleInterval(TimeSpan value)
        //{
        //    if (value.TotalMilliseconds == 0)
        //    {
        //        return null;
        //    }
        //    var doubleValue = value.TotalMilliseconds / 1000 / 60 / 60;
        //    return double.Parse(String.Format("{0:0.00}", doubleValue));
        //}
    }
}
