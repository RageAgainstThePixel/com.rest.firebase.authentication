namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Updates specified fields for the user's account.
    /// </summary>
    internal class SetAccountInfo : FirebaseRequestBase<SetAccountInfoRequest, SetAccountInfoResponse>
    {
        public SetAccountInfo(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSetAccountUrl;
    }
}
