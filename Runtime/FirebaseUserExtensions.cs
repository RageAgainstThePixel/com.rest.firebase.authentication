// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Firebase.Authentication
{
    public static class FirebaseUserExtensions
    {
        /// <summary>
        /// Returns the <see cref="FirebaseUser.Uid"/> as a <see cref="Guid"/>.
        /// </summary>
        /// <param name="firebaseUser">Firebase user.</param>
        /// <returns><see cref="Guid"/> of <see cref="FirebaseUser.Uid"/>.</returns>
        public static Guid GetUuid(this FirebaseUser firebaseUser) => firebaseUser.Uid.GenerateGuid();
    }
}
