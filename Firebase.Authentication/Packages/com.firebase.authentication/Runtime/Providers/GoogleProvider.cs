namespace Firebase.Authentication.Providers
{
    public class GoogleProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string token, OAuthCredentialTokenType tokenType = OAuthCredentialTokenType.AccessToken)
            => GetCredential(FirebaseProviderType.Google, token, tokenType);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Google;

        protected override string[] DefaultScopes { get; } = {
            "profile",
            "email"
        };

        protected override string LocaleParameterName => "hl";
    }
}
