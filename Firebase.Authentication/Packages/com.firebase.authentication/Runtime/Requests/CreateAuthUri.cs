namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Creates oauth authentication uri that user needs to navigate to in order to authenticate.
    /// </summary>
    internal class CreateAuthUri : FirebaseRequestBase<CreateAuthUriRequest, CreateAuthUriResponse>
    {
        public CreateAuthUri(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleCreateAuthUrl;
    }
}
