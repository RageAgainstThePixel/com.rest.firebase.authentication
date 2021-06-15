namespace Firebase.Authentication.Providers
{
    public class GithubProvider : OAuthProvider
    {
        public static AuthCredential GetCredential(string accessToken)
            => GetCredential(FirebaseProviderType.Github, accessToken, OAuthCredentialTokenType.AccessToken);

        public override FirebaseProviderType ProviderType => FirebaseProviderType.Github;

        /// <inheritdoc />
        protected override string[] DefaultScopes => null;
    }
}
