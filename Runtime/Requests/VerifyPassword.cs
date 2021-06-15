namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Verifies specified password matches the user's actual password.
    /// </summary>
    internal class VerifyPassword : FirebaseRequestBase<VerifyPasswordRequest, VerifyPasswordResponse>
    {
        public VerifyPassword(FirebaseConfiguration config)
            : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GooglePasswordUrl;
    }
}
