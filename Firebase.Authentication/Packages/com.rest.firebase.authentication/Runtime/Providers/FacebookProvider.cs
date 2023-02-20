// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Firebase.Authentication.Providers
{
    public class FacebookProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken)
            => GetCredential(FirebaseProviderType.Facebook, accessToken, OAuthCredentialTokenType.AccessToken);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Facebook;

        protected override List<string> defaultScopes { get; } = new List<string>
        {
            "email"
        };

        protected override string LocaleParameterName => "locale";
    }
}
