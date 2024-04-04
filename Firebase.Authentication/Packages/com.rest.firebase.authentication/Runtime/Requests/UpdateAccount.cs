// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Rest.Authentication.Requests
{
    internal class UpdateAccount : FirebaseRequestBase<UpdateAccountRequest, UpdateAccountResponse>
    {
        public UpdateAccount(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleAccountsUpdateUrl;
    }
}
