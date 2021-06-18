// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class RefreshToken : FirebaseRequestBase<RefreshTokenRequest, RefreshTokenResponse>
    {
        public RefreshToken(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleRefreshAuth;
    }
}
