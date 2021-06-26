// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class SetAccountInfo : FirebaseRequestBase<SetAccountInfoRequest, SetAccountInfoResponse>
    {
        public SetAccountInfo(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleAccountsUpdateUrl;
    }
}
