using System.Security.Cryptography;
using System.Text;

namespace WebBlog.Utils
{
    public static class PasswordHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmacAlg = new HMACSHA512())
            {
                passwordSalt = hmacAlg.Key;
                passwordHash = hmacAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmacAlg = new HMACSHA512(storedSalt))
            {
                var computedHash = hmacAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
