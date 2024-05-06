using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using System.Text;
using System.Threading.Tasks;

namespace Models.Services.PasswordHash
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public string Hash(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(KeySize);
            }
            byte[] dst = new byte[(SaltSize + KeySize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltSize + 1, KeySize);
            return Convert.ToBase64String(dst);

        }

        public bool Verify(string HashedPassword, string inputPassword)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltSize + KeySize) + 1;

            if (HashedPassword == null)
            {
                return false;
            }

            if (inputPassword == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(HashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltSize);

            byte[] _currentHashBytes = new byte[KeySize];
            Buffer.BlockCopy(src, SaltSize + 1, _currentHashBytes, 0, KeySize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(inputPassword, _currentSaltBytes, Iterations))
            {
                _passwordHashBytes = bytes.GetBytes(KeySize);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);
        }
        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
