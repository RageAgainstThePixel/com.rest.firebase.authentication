// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Firebase.Rest.Authentication.Providers
{
    public class MicrosoftProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken)
            => GetCredential(FirebaseProviderType.Microsoft, accessToken, OAuthCredentialTokenType.AccessToken);

        /// <inheritdoc />
        public override FirebaseProviderType ProviderType => FirebaseProviderType.Microsoft;

        protected override List<string> defaultScopes { get; } = new List<string>
        {
            "profile",
            "email",
            "openid",
            "User.Read"
        };
    }
}
