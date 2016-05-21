using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.PasswordHasher
{
    public class HashResult
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    public interface IPasswordHasher
    {
        HashResult GenerateHash(string plainText);
        bool VerifyHash(string plainText, string hash, string salt);
    }

    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltLength = 128;

        public HashResult GenerateHash(string plainText)
        {
            var salt = new byte[SaltLength];
            new Random().NextBytes(salt);

            var saltString = new string(Encoding.ASCII.GetChars(salt));

            return new HashResult
            {
                Salt = saltString,
                Hash = GenerateHash(plainText, saltString)
            };
        }

        private string GenerateHash(string plainText, string saltString)
        {
            var textAndSalt = plainText + saltString;
            var textAndSaltBytes = Encoding.Unicode.GetBytes(textAndSalt);

            using (var sha512 = SHA512.Create())
            {
                var hash = sha512.ComputeHash(textAndSaltBytes);

                return Encoding.ASCII.GetString(hash);
            }
        }

        public bool VerifyHash(string plainText, string hash, string salt)
        {
            return GenerateHash(plainText, salt) == hash;
        }
    }
}
