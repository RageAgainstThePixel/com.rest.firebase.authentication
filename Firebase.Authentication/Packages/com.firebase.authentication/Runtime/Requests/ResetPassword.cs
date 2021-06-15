namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Resets user's password for given email by sending a reset email.
    /// </summary>
    internal class ResetPassword : FirebaseRequestBase<ResetPasswordRequest, ResetPasswordResponse>
    {
        public ResetPassword(FirebaseConfiguration config)
            : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleGetConfirmationCodeUrl;
    }
}
