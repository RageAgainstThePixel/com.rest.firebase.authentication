// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class AccountInfoUri : FirebaseRequestBase<CreateAuthUriRequest, CreateAuthUriResponse>
    {
        public AccountInfoUri(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleAccountsCreateAuthUrl;
    }
}
