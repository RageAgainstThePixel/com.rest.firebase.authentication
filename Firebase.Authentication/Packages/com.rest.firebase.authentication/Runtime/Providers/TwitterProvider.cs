// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Requests;
using System.Collections.Generic;

namespace Firebase.Authentication.Providers
{
    public class TwitterProvider : OAuthProvider
    {
        public override FirebaseProviderType ProviderType => FirebaseProviderType.Twitter;

        protected override List<string> defaultScopes => new List<string>();

        protected override string LocaleParameterName => "lang";

        internal override AuthCredential GetCredential(VerifyAssertionResponse response)
            => new TwitterCredential(response.OauthAccessToken, response.OauthTokenSecret);

        private class TwitterCredential : OAuthCredential
        {
            private string Secret { get; }

            internal override string GetPostBodyValue(FirebaseProviderType providerType)
            {
                var value = base.GetPostBodyValue(providerType);
                return value == null ? null : $"{value}&oauth_token_secret={Secret}";
            }

            /// <inheritdoc />
            public TwitterCredential(string accessToken, string secret)
                : base(accessToken, OAuthCredentialTokenType.AccessToken, FirebaseProviderType.Twitter)
            {
                Secret = secret;
            }
        }
    }
}
