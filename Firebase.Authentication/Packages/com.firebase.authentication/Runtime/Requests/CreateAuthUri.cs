// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Creates oauth authentication uri that user needs to navigate to in order to authenticate.
    /// </summary>
    internal class CreateAuthUri : FirebaseRequestBase<CreateAuthUriRequest, CreateAuthUriResponse>
    {
        public CreateAuthUri(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleCreateAuthUrl;
    }
}
