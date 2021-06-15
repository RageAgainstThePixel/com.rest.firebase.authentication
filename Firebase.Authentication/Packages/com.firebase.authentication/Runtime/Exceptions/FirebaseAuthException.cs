using System;
namespace Firebase.Authentication.Exceptions
{
    /// <summary>
    /// Base class for all FirebaseAuth exceptions.
    /// </summary>
    public class FirebaseAuthException : Exception
    {
        internal FirebaseAuthException(string message, AuthErrorReason reason)
            : base(message)
        {
            Reason = reason;
        }

        internal FirebaseAuthException(string message, Exception innerException, AuthErrorReason reason)
            : base(message, innerException)
        {
            Reason = reason;
        }

        internal FirebaseAuthException(Exception innerException, AuthErrorReason reason)
            : this($"Firebase exception occurred: {reason}", innerException, reason)
        {
        }

        /// <summary>
        /// Indicates why a login failed. If not resolved, defaults to <see cref="AuthErrorReason.Undefined"/>.
        /// </summary>
        public AuthErrorReason Reason { get; }
    }
}
