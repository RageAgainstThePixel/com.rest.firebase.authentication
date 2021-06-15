namespace Firebase.Authentication.Exceptions
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