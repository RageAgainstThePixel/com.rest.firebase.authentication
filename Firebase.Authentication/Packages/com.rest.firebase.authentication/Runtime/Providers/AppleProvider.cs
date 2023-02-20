// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Firebase.Authentication.Providers
{
    public class AppleProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken, OAuthCredentialTokenType tokenType = OAuthCredentialTokenType.AccessToken)
            => GetCredential(FirebaseProviderType.Apple, accessToken, tokenType);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Apple;

        protected override List<string> defaultScopes { get; } = new List<string>
        {
            "email"
        };

        protected override string LocaleParameterName => "locale";
    }
}
