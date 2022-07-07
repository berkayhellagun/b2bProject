using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public static class HashHelper
    {
        public static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var key = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = key.Key;
                // we need convert to string to byte for compute hash
                passwordHash = key.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash == passwordHash;
            }
        }
    }
}
