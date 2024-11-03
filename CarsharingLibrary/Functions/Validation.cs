using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CarsharingLibrary.Functions
{
    public static partial class Validation
    {
        private static readonly Regex EmailRegex = EmailRegularExpressioin();

        public static bool ValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }

        private static readonly Regex PhoneRegex = PhoneRegularExopression();

        public static bool ValidPhone(string phone)
        {
            return PhoneRegex.IsMatch(phone);
        }

        public static string GetHashString(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(bytes);
            return string.Concat(hashBytes.Select(b => b.ToString("x2")));
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
        private static partial Regex EmailRegularExpressioin();

        [GeneratedRegex(@"^(\+7|8)[\s-]?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}$", RegexOptions.Compiled)]
        private static partial Regex PhoneRegularExopression();
    }
}

