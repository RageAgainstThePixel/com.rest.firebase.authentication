using System.Collections.Generic;

namespace Firebase.Authentication.Providers
{
    public class AppleProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken)
            => GetCredential(FirebaseProviderType.Apple, accessToken, OAuthCredentialTokenType.AccessToken);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Apple;

        protected override List<string> defaultScopes { get; } = new List<string>
        {
            "email"
        };

        protected override string LocaleParameterName => "locale";
    }
}
