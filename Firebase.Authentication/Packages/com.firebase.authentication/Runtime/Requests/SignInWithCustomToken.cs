// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class SignInWithCustomToken : FirebaseRequestBase<SignInCustomTokenRequest, SignInCustomTokenResponse>
    {
        public SignInWithCustomToken(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSignInWithCustomToken;
    }
}
