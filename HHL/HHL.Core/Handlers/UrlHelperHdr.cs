using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HHL.Core.Handlers
{
    public static class UrlHelperHdr
    {
        public static string ToUrl(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            return Regex.Replace(s, @"\s+", "-").ToLower();
        }

        public static string FromUrl(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            return s.Replace('-',' ');
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
