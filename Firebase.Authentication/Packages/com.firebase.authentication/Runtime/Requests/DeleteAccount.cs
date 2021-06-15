namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Deletes user's account.
    /// </summary>
    internal class DeleteAccount : FirebaseRequestBase<IdTokenRequest, object>
    {
        public DeleteAccount(FirebaseConfiguration config) : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleDeleteUserUrl;
    }
}
