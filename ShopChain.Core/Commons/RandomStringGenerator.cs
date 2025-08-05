using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShopChain.Core.Commons
{
    public class Helper
    {
        private static readonly Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Thêm chữ thường vào đây

        public static string GenerateRandomString(int length)
        {
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }


        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public static bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            string hashToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordToCheck,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedHash == hashToCheck;
        }
    }

    class RandomWithoutRandom
    {
        public static int GenerateRandom(int min = 0, int max = 100)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max phải lớn hơn Min.");
            }

            Random random = new Random();
            int result = random.Next(min, max);

            return result;
        }

        public static decimal GenerateRandomDecimal(decimal min = 100000, decimal max = 1000000)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max phải lớn hơn Min.");
            }

            Random random = new Random();

            double factor = random.NextDouble();

            decimal result = min + (decimal)factor * (max - min);

            result = Math.Floor(result);

            return result;
        }
    }
}
