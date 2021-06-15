namespace Firebase.Authentication.Providers
{
    public class FacebookProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken) => GetCredential(FirebaseProviderType.Facebook, accessToken, OAuthCredentialTokenType.AccessToken);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Facebook;

        protected override string[] DefaultScopes { get; } =
        {
            "email"
        };

        protected override string LocaleParameterName => "locale";
    }
}
