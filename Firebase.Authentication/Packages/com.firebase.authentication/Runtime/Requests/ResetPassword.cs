namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Resets user's password for given email by sending a reset email.
    /// </summary>
    internal class ResetPassword : FirebaseRequestBase<ResetPasswordRequest, ResetPasswordResponse>
    {
        public ResetPassword(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleGetConfirmationCodeUrl;
    }
}
