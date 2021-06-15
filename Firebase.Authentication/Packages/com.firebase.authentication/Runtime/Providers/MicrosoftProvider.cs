namespace Firebase.Authentication.Providers
{
    public class MicrosoftProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken)
            => GetCredential(FirebaseProviderType.Microsoft, accessToken, OAuthCredentialTokenType.AccessToken);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Microsoft;

        protected override string[] DefaultScopes { get; } =
        {
            "profile",
            "email",
            "openid",
            "User.Read"
        };
    }
}
