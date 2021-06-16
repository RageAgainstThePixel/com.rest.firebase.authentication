// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using Firebase.Authentication.Requests;
using System.Linq;
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
            var request = new ResetPasswordRequest
            {
                Email = email
            };

            return resetPassword.ExecuteAsync(request);
        }

        internal Task<FirebaseUser> SignInUserAsync(string email, string password)
            => SignInWithCredentialAsync(GetCredential(email, password));

        internal async Task<FirebaseUser> SignUpUserAsync(string email, string password, string displayName)
        {
            var signupResponse = await signupNewUser.ExecuteAsync(new SignupNewUserRequest
            {
                Email = email,
                Password = password,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            var credential = new FirebaseCredential
            {
                ExpiresIn = signupResponse.ExpiresIn,
                IdToken = signupResponse.IdToken,
                RefreshToken = signupResponse.RefreshToken,
                ProviderType = FirebaseProviderType.EmailAndPassword
            };

            // set display name if available
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var setResponse = await setAccountInfo.ExecuteAsync(new SetAccountDisplayName
                {
                    DisplayName = displayName,
                    IdToken = signupResponse.IdToken,
                    ReturnSecureToken = true
                }).ConfigureAwait(false);

                var setUser = new UserInfo
                {
                    DisplayName = setResponse.DisplayName,
                    Email = setResponse.Email,
                    IsEmailVerified = setResponse.EmailVerified,
                    Uid = setResponse.LocalId,
                    IsAnonymous = false
                };

                return new FirebaseUser(Configuration, setUser, credential);
            }

            var getUser = await GetUserInfoAsync(signupResponse.IdToken).ConfigureAwait(false);

            return new FirebaseUser(Configuration, getUser, credential);
        }

        protected internal override async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            var ec = (EmailCredential)credential;

            var response = await verifyPassword.ExecuteAsync(new VerifyPasswordRequest
            {
                Email = ec.Email,
                Password = ec.Password,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            var user = await GetUserInfoAsync(response.IdToken).ConfigureAwait(false);
            var fc = new FirebaseCredential
            {
                ExpiresIn = response.ExpiresIn,
                IdToken = response.IdToken,
                RefreshToken = response.RefreshToken,
                ProviderType = FirebaseProviderType.EmailAndPassword
            };

            return new FirebaseUser(Configuration, user, fc);
        }

        protected internal override async Task<FirebaseUser> LinkWithCredentialAsync(string token, AuthCredential credential)
        {
            var c = (EmailCredential)credential;
            var request = new SetAccountLinkRequest
            {
                IdToken = token,
                Email = c.Email,
                Password = c.Password,
                ReturnSecureToken = true
            };

            SetAccountLinkResponse link;

            try
            {
                link = await linkAccount.ExecuteAsync(request).ConfigureAwait(false);
            }
            catch (FirebaseAuthException e) when (e.Reason == AuthErrorReason.EmailExists)
            {
                throw new FirebaseAuthWithCredentialException("The email address is already in use by another account.", credential, AuthErrorReason.EmailExists);
            }

            var getResult = await getAccountInfo.ExecuteAsync(new IdTokenRequest { IdToken = link.IdToken }).ConfigureAwait(false);

            var u = getResult.Users[0];
            var info = new UserInfo
            {
                DisplayName = u.DisplayName,
                Email = u.Email,
                IsEmailVerified = u.EmailVerified,
                FederatedId = u.ProviderUserInfo?.FirstOrDefault(i => i.FederatedId != null)?.FederatedId,
                Uid = u.LocalId,
                PhotoUrl = u.PhotoUrl,
                IsAnonymous = false
            };

            var fc = new FirebaseCredential
            {
                ExpiresIn = link.ExpiresIn,
                IdToken = link.IdToken,
                ProviderType = credential.ProviderType,
                RefreshToken = link.RefreshToken
            };

            return new FirebaseUser(Configuration, info, fc);
        }

        private async Task<UserInfo> GetUserInfoAsync(string idToken)
        {
            var getResponse = await getAccountInfo.ExecuteAsync(new IdTokenRequest { IdToken = idToken }).ConfigureAwait(false);
            var user = getResponse.Users[0];

            return new UserInfo
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                IsEmailVerified = user.EmailVerified,
                Uid = user.LocalId,
                PhotoUrl = user.PhotoUrl,
                IsAnonymous = false
            };
        }

        private class EmailCredential : AuthCredential
        {
            public EmailCredential(string email, string password) : base(FirebaseProviderType.EmailAndPassword)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; }

            public string Password { get; }
        }
    }
}
