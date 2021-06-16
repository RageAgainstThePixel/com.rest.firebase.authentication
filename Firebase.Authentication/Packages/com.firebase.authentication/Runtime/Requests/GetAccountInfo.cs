// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Gets basic info about a user and their account.
    /// </summary>
    internal class GetAccountInfo : FirebaseRequestBase<IdTokenRequest, GetAccountInfoResponse>
    {
        public GetAccountInfo(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleGetUser;
    }

}
