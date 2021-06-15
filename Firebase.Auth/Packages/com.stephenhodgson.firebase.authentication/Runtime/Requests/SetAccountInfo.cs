namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Updates specified fields for the user's account.
    /// </summary>
    internal class SetAccountInfo : FirebaseRequestBase<SetAccountInfoRequest, SetAccountInfoResponse>
    {
        public SetAccountInfo(FirebaseConfiguration config)
            : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSetAccountUrl;
    }
}
