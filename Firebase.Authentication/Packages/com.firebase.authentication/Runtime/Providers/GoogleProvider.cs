using System.Collections.Generic;

namespace Firebase.Authentication.Providers
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
