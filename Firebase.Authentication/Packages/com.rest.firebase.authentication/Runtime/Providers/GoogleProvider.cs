// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Firebase.Rest.Authentication.Providers
{
    public class GoogleProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string token, OAuthCredentialTokenType tokenType = OAuthCredentialTokenType.AccessToken)
            => GetCredential(FirebaseProviderType.Google, token, tokenType);

        /// <inheritdoc />
        public override FirebaseProviderType ProviderType => FirebaseProviderType.Google;

        protected override List<string> defaultScopes { get; } = new List<string>
        {
            "profile",
            "email"
        };

        protected override string LocaleParameterName => "hl";
    }
}
