// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class CreateAuthUri : FirebaseRequestBase<CreateAuthUriRequest, CreateAuthUriResponse>
    {
        public CreateAuthUri(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleCreateAuthUrl;
    }
}
