// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace Firebase.Authentication
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Generates a <see cref="Guid"/> based on the string.
        /// </summary>
        /// <param name="string">The string to generate the <see cref="Guid"/>.</param>
        /// <returns>A new <see cref="Guid"/> that represents the string.</returns>
        internal static Guid GenerateGuid(this string @string)
        {
            using (MD5 md5 = MD5.Create())
            {
                return new Guid(md5.ComputeHash(Encoding.Default.GetBytes(@string)));
            }
        }
    }
}
