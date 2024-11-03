using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CarsharingLibrary.Functions
{
    public static class Validation
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static bool ValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }

        private static readonly Regex PhoneRegex = new Regex(@"^(\+7|8)[\s-]?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}$", RegexOptions.Compiled);

        public static bool ValidPhone(string phone)
        {
            return PhoneRegex.IsMatch(phone);
        }

        public static string GetHashString(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha256.ComputeHash(bytes);
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }
    }
}

