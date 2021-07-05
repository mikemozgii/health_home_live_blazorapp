using System;
using System.Text;


namespace HHL.Auth.Core.Services
{
    public class Base64Svc
    {
        public string Base64_Encode(string input, string salt = null)
        {
            if (!String.IsNullOrWhiteSpace(salt))
            {
                input = input + salt;
            }

            var saltedHashBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(saltedHashBytes);
        }

        public string Base64_Decode(string input, string salt = null)
        {

            var tempBytes = Convert.FromBase64String(input);
            var tempValue = Encoding.UTF8.GetString(tempBytes);
            if (!String.IsNullOrWhiteSpace(salt))
            {
                return tempValue.Replace(salt, "");
            }
            return tempValue;
        }
    }
}
