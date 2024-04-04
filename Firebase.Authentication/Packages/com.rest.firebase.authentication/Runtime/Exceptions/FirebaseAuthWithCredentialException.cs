// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Rest.Authentication.Exceptions
{
    public class FirebaseAuthWithCredentialException : FirebaseAuthException
    {
        internal FirebaseAuthWithCredentialException(string message, AuthCredential credential, AuthErrorReason reason)
            : base(message, reason)
        {
            Credential = credential;
        }

        internal FirebaseAuthWithCredentialException(string message, string email, AuthCredential credential, AuthErrorReason reason)
            : base(message, reason)
        {
            Credential = credential;
            Email = email;
        }

        public AuthCredential Credential { get; }

        public string Email { get; }
    }
}
