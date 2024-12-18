using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class PasswordHasher
    {
        // Method to hash a password using SHA256
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Method to verify a hashed password against a plain password
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            var hashedPlainPassword = HashPassword(plainPassword);
            return hashedPlainPassword == hashedPassword;
        }
    }
}
