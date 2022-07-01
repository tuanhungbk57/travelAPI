﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Helper
{
    public class PasswordHashHelper
    {
        public static byte[] GetSecureSalt()
        {
            // Starting .NET 6, the Class RNGCryptoServiceProvider is obsolete,
            // so now we have to use the RandomNumberGenerator Class to generate a secure random number bytes

            return RandomNumberGenerator.GetBytes(32);
        }
        public static string HashUsingPbkdf2(string password, byte[] salt)
        {
            byte[] derivedKey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 100000, 32);

            return Convert.ToBase64String(derivedKey);
        }
    }
}
