using System;
using System.Security.Cryptography;
using System.Text;
using HashAlgorithm = HHL.Auth.Core.Enums.HashAlgorithm;

namespace HHL.Auth.Core.Services
{
    public class HashSvc
    {
        public string Compute(string plainText, Enums.HashAlgorithm hashAlgorithm, byte[] saltBytes = null)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                //// Define min and max salt sizes.
                //int minSaltSize = 4;
                //int maxSaltSize = 8;

                //// Generate a random number for the size of the salt.
                //Random random = new Random();
                //int saltSize = random.Next(minSaltSize, maxSaltSize);

                //// Allocate a byte array, which will hold the salt.
                //saltBytes = new byte[saltSize];

                //// Initialize a random number generator.
                //var rng = new RNGCryptoServiceProvider();

                //// Fill the salt with cryptographically strong byte values.
                //rng.GetNonZeroBytes(saltBytes);
                saltBytes = new SaltSvc().GenerateRandom();
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            System.Security.Cryptography.HashAlgorithm hash;

            switch (hashAlgorithm)
            {
                case HashAlgorithm.SHA1:
                    hash = new SHA1Managed();
                    break;

                case HashAlgorithm.SHA256:
                    hash = new SHA256Managed();
                    break;

                case HashAlgorithm.SHA384:
                    hash = new SHA384Managed();
                    break;

                case HashAlgorithm.SHA512:
                    hash = new SHA512Managed();
                    break;
                case HashAlgorithm.MD5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                default:
                    hash = new SHA256Managed();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public bool Verify(string plainText, Enums.HashAlgorithm hashAlgorithm, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            // Make sure that hashing algorithm name is specified.


            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm)
            {
                case HashAlgorithm.SHA1:
                    hashSizeInBits = 160;
                    break;

                case HashAlgorithm.SHA256:
                    hashSizeInBits = 256;
                    break;

                case HashAlgorithm.SHA384:
                    hashSizeInBits = 384;
                    break;

                case HashAlgorithm.SHA512:
                    hashSizeInBits = 512;
                    break;
                case HashAlgorithm.MD5:
                    hashSizeInBits = 128;
                    break;
                default:
                    hashSizeInBits = 256;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = Compute(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }
    }
}
