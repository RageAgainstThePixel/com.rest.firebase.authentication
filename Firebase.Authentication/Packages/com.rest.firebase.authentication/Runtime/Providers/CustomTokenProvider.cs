// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using Firebase.Rest.Authentication.Requests;

namespace Firebase.Rest.Authentication.Providers
{
    public class CustomTokenProvider : FirebaseAuthProvider
    {
        private SignInWithCustomToken signInWithCustomToken;
        private GetAccountInfo getAccountInfo;
        private VerifyAssertion verifyAssertion;

        public override FirebaseProviderType ProviderType => FirebaseProviderType.CustomToken;

        internal override void Initialize(FirebaseConfiguration configuration)
        {
            base.Initialize(configuration);

            signInWithCustomToken = new SignInWithCustomToken(Configuration);
            getAccountInfo = new GetAccountInfo(Configuration);
            verifyAssertion = new VerifyAssertion(Configuration);
        }

        private static AuthCredential GetCredential(string customToken)
            => new CustomTokenCredential(customToken);

        internal Task<FirebaseUser> SignInWithCustomTokenAsync(string customToken)
            => SignInWithCredentialAsync(GetCredential(customToken));

        protected internal override async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            var customTokenCredential = (CustomTokenCredential)credential;

            var response = await signInWithCustomToken.ExecuteAsync(new SignInCustomTokenRequest(customTokenCredential.CustomToken)).ConfigureAwait(false);

            return new FirebaseUser(
                Configuration,
                await GetUserInfoAsync(response.IdToken).ConfigureAwait(false),
                new FirebaseCredential(
                    response.IdToken,
                    response.RefreshToken,
                    response.ExpiresIn,
                    FirebaseProviderType.CustomToken));
        }

        private async Task<UserInfo> GetUserInfoAsync(string idToken)
        {
            var getResponse = await getAccountInfo.ExecuteAsync(
                    new IdTokenRequest(idToken))
                .ConfigureAwait(false);
            return new UserInfo(getResponse.Users[0]);
        }

        protected internal override async Task<FirebaseUser> LinkWithCredentialAsync(string idToken, AuthCredential credential)
        {
            var authCredential = (CustomTokenCredential)credential;
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(credential.ProviderType,
                    new VerifyAssertionRequest(
                        idToken,
                        $"https://{Configuration.AuthDomain}",
                        $"bearer={authCredential.CustomToken}&providerId={FirebaseProviderType.CustomToken.ToEnumMemberString()}",
                        authCredential.CustomToken))
                .ConfigureAwait(false);
            credential = GetCredential(authCredential.CustomToken);
            response.Validate(credential);
            return user;
        }

        private class CustomTokenCredential : AuthCredential
        {
            public CustomTokenCredential(string customToken)
                : base(FirebaseProviderType.CustomToken)
            {
                CustomToken = customToken;
            }

            public string CustomToken { get; }
        }
    }
}
