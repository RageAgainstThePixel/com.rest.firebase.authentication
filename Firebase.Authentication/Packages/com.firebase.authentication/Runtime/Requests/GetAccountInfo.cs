namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Gets basic info about a user and his/her account.
    /// </summary>
    internal class GetAccountInfo : FirebaseRequestBase<IdTokenRequest, GetAccountInfoResponse>
    {
        public GetAccountInfo(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleGetUser;
    }

}
