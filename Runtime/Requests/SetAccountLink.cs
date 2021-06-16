namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Link two accounts.
    /// </summary>
    internal class SetAccountLink : FirebaseRequestBase<SetAccountLinkRequest, SetAccountLinkResponse>
    {
        public SetAccountLink(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSetAccountUrl;
    }
}
