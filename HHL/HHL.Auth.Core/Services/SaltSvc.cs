using System;
using System.Security.Cryptography;
using System.Text;

namespace HHL.Auth.Core.Services
{
    public class SaltSvc
    {
        public byte[] GenerateRandom()
        {
            var minSaltSize = 4;
            var maxSaltSize = 8;


            var random = new Random();
            var saltSize = random.Next(minSaltSize, maxSaltSize);
            var saltBytes = new byte[saltSize];
            var rng = new RNGCryptoServiceProvider();
            new RNGCryptoServiceProvider().GetNonZeroBytes(saltBytes);
            return saltBytes;
        }

        public byte[] Generate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            return Encoding.ASCII.GetBytes(input);
        }
    }
}
