namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Link two accounts.
    /// </summary>
    internal class SetAccountLink : FirebaseRequestBase<SetAccountLinkRequest, SetAccountLinkResponse>
    {
        public SetAccountLink(FirebaseConfiguration config)
            : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSetAccountUrl;
    }
}
