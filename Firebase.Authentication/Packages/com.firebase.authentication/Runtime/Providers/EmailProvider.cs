// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using Firebase.Authentication.Requests;
using System.Threading.Tasks;

namespace Firebase.Authentication.Providers
{
    public class EmailProvider : FirebaseAuthProvider
    {
        private SignupNewUser signupNewUser;
        private SetAccountInfo setAccountInfo;
        private GetAccountInfo getAccountInfo;
        private VerifyPassword verifyPassword;
        private ResetPassword resetPassword;
        private SetAccountLink linkAccount;

        public override FirebaseProviderType ProviderType => FirebaseProviderType.EmailAndPassword;

        internal override void Initialize(FirebaseConfiguration configuration)
        {
            base.Initialize(configuration);

            signupNewUser = new SignupNewUser(Configuration);
            setAccountInfo = new SetAccountInfo(Configuration);
            getAccountInfo = new GetAccountInfo(Configuration);
            verifyPassword = new VerifyPassword(Configuration);
            resetPassword = new ResetPassword(Configuration);
            linkAccount = new SetAccountLink(configuration);
        }

        private static AuthCredential GetCredential(string email, string password)
            => new EmailCredential(email, password);

        internal Task ResetEmailPasswordAsync(string email)
        {
            var request = new ResetPasswordRequest(email);
            return resetPassword.ExecuteAsync(request);
        }

        internal Task<FirebaseUser> SignInUserAsync(string email, string password)
            => SignInWithCredentialAsync(GetCredential(email, password));

        internal async Task<FirebaseUser> SignUpUserAsync(string email, string password, string displayName)
        {
            var signupResponse = await signupNewUser.ExecuteAsync(new SignupNewUserRequest(email, password, true)).ConfigureAwait(false);

            var credential = new FirebaseCredential(
                signupResponse.IdToken,
                signupResponse.RefreshToken,
                signupResponse.ExpiresIn,
                FirebaseProviderType.EmailAndPassword);

            // set display name if available
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var setResponse = await setAccountInfo.ExecuteAsync(
                    new SetAccountDisplayName(
                        signupResponse.IdToken,
                        true,
                        displayName))
                    .ConfigureAwait(false);

                return new FirebaseUser(
                    Configuration,
                    new UserInfo(setResponse),
                    credential);
            }

            return new FirebaseUser(
                Configuration,
                await GetUserInfoAsync(signupResponse.IdToken).ConfigureAwait(false),
                credential);
        }

        protected internal override async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            var emailCredential = (EmailCredential)credential;

            VerifyPasswordResponse response = await verifyPassword.ExecuteAsync(new VerifyPasswordRequest(emailCredential.Email, emailCredential.Password, true)).ConfigureAwait(false);

            return new FirebaseUser(
                Configuration,
                await GetUserInfoAsync(response.IdToken).ConfigureAwait(false),
                new FirebaseCredential(
                    response.IdToken,
                    response.RefreshToken,
                    response.ExpiresIn,
                    FirebaseProviderType.EmailAndPassword));
        }

        protected internal override async Task<FirebaseUser> LinkWithCredentialAsync(string token, AuthCredential credential)
        {
            var emailCredential = (EmailCredential)credential;
            var linkRequest = new SetAccountLinkRequest(
                token,
                true,
                emailCredential.Email,
                emailCredential.Password);

            SetAccountLinkResponse linkResponse;

            try
            {
                linkResponse = await linkAccount.ExecuteAsync(linkRequest).ConfigureAwait(false);
            }
            catch (FirebaseAuthException e) when (e.Reason == AuthErrorReason.EmailExists)
            {
                throw new FirebaseAuthWithCredentialException("The email address is already in use by another account.", credential, AuthErrorReason.EmailExists);
            }

            var getResult = await getAccountInfo.ExecuteAsync(new IdTokenRequest(linkResponse.IdToken)).ConfigureAwait(false);

            return new FirebaseUser(
                Configuration,
                new UserInfo(getResult.Users[0]),
                new FirebaseCredential(
                    linkResponse.IdToken,
                    linkResponse.RefreshToken,
                    linkResponse.ExpiresIn,
                    credential.ProviderType));
        }

        private async Task<UserInfo> GetUserInfoAsync(string idToken)
        {
            var getResponse = await getAccountInfo.ExecuteAsync(
                new IdTokenRequest(idToken))
                .ConfigureAwait(false);
            return new UserInfo(getResponse.Users[0]);
        }

        private class EmailCredential : AuthCredential
        {
            public EmailCredential(string email, string password)
                : base(FirebaseProviderType.EmailAndPassword)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; }

            public string Password { get; }
        }
    }
}
