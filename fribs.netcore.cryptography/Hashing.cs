using ServiceStack;
using System;
using System.Security.Cryptography;

namespace fribs.netcore.cryptography
{
    public class Hashing
    {
        private static readonly RNGCryptoServiceProvider RandomByteGenerator = new RNGCryptoServiceProvider();
        private static readonly HashAlgorithm HashAlgorithm = new SHA256Managed();
        private const int SaltSize = 32; //SHA256 is 32 bytes so Salt should be equivalent

        public static string GetRandomBase64String()
        {
            byte[] saltBytes;
            RandomByteGenerator.GetBytes(saltBytes = new byte[SaltSize]);
            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateHash(string password, string salt)
        {
            var hashedPassBytes = HashPasswordWithSalt(password.ToUtf8Bytes(), Convert.FromBase64String(salt));
            return Convert.ToBase64String(hashedPassBytes);
        }

        public static bool DoesStringMatchHash(string stringEntered, string hashToMatch, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            //hash entered password with salt
            var hashedEnteredPasswordBytes = HashPasswordWithSalt(stringEntered.ToUtf8Bytes(), saltBytes);

            var enteredPassHash = Convert.ToBase64String(hashedEnteredPasswordBytes);

            return hashToMatch == enteredPassHash;
        }

        private static byte[] HashPasswordWithSalt(byte[] password, byte[] salt)
        {
            //create hash generator and hash bytes
            var plainTextWithSaltByteArray = new byte[password.Length + salt.Length];

            Array.Copy(salt, 0, plainTextWithSaltByteArray, 0, salt.Length);
            Array.Copy(password, 0, plainTextWithSaltByteArray, salt.Length, password.Length);

            return HashAlgorithm.ComputeHash(plainTextWithSaltByteArray);
        }

        public static string GetPin()
        {
            var pin = "";
            var random = new Random();
            for (var i = 0; i < 4; i++)
            {
                pin += random.Next(0, 9).ToString();
            }

            return pin;
        }
    }
}
