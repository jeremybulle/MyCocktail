using System;
using System.Linq;
using System.Security.Cryptography;

namespace MyCocktail.Domain.Helper
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Size = 1000;

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <returns>Hashed pasword as <see langword="string"/></returns>
        public static string Hash(string password)
        {
            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Size,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Size}.{salt}.{key}";
            }
        }

        /// <summary>
        /// Allow to know if a clear password is equal to the same password hashed
        /// </summary>
        /// <param name="hash">Hashed password</param>
        /// <param name="password">Clear passord</param>
        /// <returns><see langword="true"/> if clear password and hashed password are equal, <see langword="false"/>  if clear password and hashed password are NOT equal</returns>
        public static bool Check(string hash, string password)
        {
            if (hash == null)
                return false;
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }

    }
}
