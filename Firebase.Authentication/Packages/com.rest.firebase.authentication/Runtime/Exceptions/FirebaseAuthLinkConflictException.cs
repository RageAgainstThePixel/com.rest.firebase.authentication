// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Firebase.Rest.Authentication.Exceptions
{
    /// <summary>
    /// Exception thrown when user tries to login with email that is already associated with a different Auth provider
    /// (and creating multiple accounts using the same email address with different authentication providers is not allowed in Firebase Console)
    /// </summary>
    public class FirebaseAuthLinkConflictException : FirebaseAuthException
    {
        internal FirebaseAuthLinkConflictException(string email, IEnumerable<FirebaseProviderType> providers)
            : base($"An account already exists with the same email address but different sign-in credentials. Sign in using a provider associated with this email address: {email}", AuthErrorReason.AccountExistsWithDifferentCredential)
        {
            Email = email;
            Providers = providers;
        }

        public string Email { get; }

        public IEnumerable<FirebaseProviderType> Providers { get; }
    }
}
