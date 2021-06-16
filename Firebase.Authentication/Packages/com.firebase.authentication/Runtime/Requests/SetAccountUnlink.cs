namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Unlink accounts.
    /// </summary>
    internal class SetAccountUnlink : FirebaseRequestBase<SetAccountUnlinkRequest, SetAccountInfoResponse>
    {
        public SetAccountUnlink(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSetAccountUrl;
    }
}
