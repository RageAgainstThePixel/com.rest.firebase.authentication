namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Deletes user's account.
    /// </summary>
    internal class DeleteAccount : FirebaseRequestBase<IdTokenRequest, object>
    {
        public DeleteAccount(FirebaseConfiguration configuraton) : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleDeleteUserUrl;
    }
}
