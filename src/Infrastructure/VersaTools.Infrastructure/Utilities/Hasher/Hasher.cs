
using System;
using System.Security.Cryptography;
using System.Text;

namespace VersaTools.Infrastructure.Utilities.Hasher
{
 
    public class Hasher
    {
        public static string HashString(string input , string key = "Abracadabra", int length = 16, bool normalizeInput = false, bool normalizeKey = false)
        {
            if (normalizeInput) input = input.ToLower();
            if (normalizeKey)  key = key.ToLower();

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hash.Substring(0, Math.Min(length, hash.Length));
            }
        }
    }
}
