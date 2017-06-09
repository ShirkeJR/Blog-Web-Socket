using System.Security.Cryptography;
using System.Text;

namespace Blog.AuthManager
{
    internal static class CryptoService
    {
        public static string GetSHA256String(this string input)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding utf8 = Encoding.UTF8;
                byte[] result = hash.ComputeHash(utf8.GetBytes(input));
                foreach (var @byte in result)
                    builder.Append(@byte.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}