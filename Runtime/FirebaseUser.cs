using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Authentication.Providers;
using Firebase.Authentication.Requests;

namespace Firebase.Authentication
{
    /// <summary>
    /// Represents a signed-in Firebase user.
    /// </summary>
    public class FirebaseUser
    {
        private const string RefreshToken = "refresh_token";

        private readonly DeleteAccount deleteAccount;
        private readonly RefreshToken token;
        private readonly UpdateAccount updateAccount;
        private readonly SetAccountUnlink unlinkAccount;
        private readonly SetAccountInfo setAccountInfo;
        private readonly FirebaseConfiguration config;

        internal FirebaseUser(FirebaseConfiguration config, UserInfo userInfo, FirebaseCredential credential)
        {
            this.config = config;
            Info = userInfo;
            Credential = credential;
            deleteAccount = new DeleteAccount(config);
            token = new RefreshToken(config);
            updateAccount = new UpdateAccount(config);
            unlinkAccount = new SetAccountUnlink(config);
            setAccountInfo = new SetAccountInfo(config);
        }

        /// <summary>
        /// Firebase user ID.
        /// </summary>
        public string Uid => Info.Uid;

        /// <summary>
        /// Specifies whether this user is anonymous.
        /// </summary>
        public bool IsAnonymous => Info.IsAnonymous;

        /// <summary>
        /// More information about current user.
        /// </summary>
        public UserInfo Info { get; private set; }

        public FirebaseCredential Credential { get; private set; }

        /// <summary>
        /// Get fresh firebase id token.
        /// </summary>
        /// <param name="forceRefresh"> Specifies whether the token should be refreshed even if it's not expired. </param>
        public async Task<string> GetIdTokenAsync(bool forceRefresh = false)
        {
            if (forceRefresh || Credential.IsExpired())
            {
                var refresh = await token.ExecuteAsync(new RefreshTokenRequest
                {
                    GrantType = RefreshToken,
                    RefreshToken = Credential.RefreshToken
                });

                Credential = new FirebaseCredential
                {
                    ExpiresIn = refresh.ExpiresIn,
                    IdToken = refresh.IdToken,
                    ProviderType = Credential.ProviderType,
                    RefreshToken = refresh.RefreshToken
                };

                await config.UserManager.UpdateExistingUserAsync(this).ConfigureAwait(false);
            }

            return Credential.IdToken;
        }

        /// <summary>
        /// Change user's password.
        /// </summary>
        /// <param name="password"> The new password. </param>
        public async Task ChangePasswordAsync(string password)
        {
            var idToken = await GetIdTokenAsync().ConfigureAwait(false);
            var result = await updateAccount.ExecuteAsync(new UpdateAccountRequest
            {
                IdToken = idToken,
                Password = password,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            Credential = new FirebaseCredential
            {
                ExpiresIn = result.ExpiresIn,
                IdToken = result.IdToken,
                ProviderType = Credential.ProviderType,
                RefreshToken = result.RefreshToken
            };

            await config.UserManager.UpdateExistingUserAsync(this).ConfigureAwait(false);
        }

        /// <summary>
        /// Change user's display name.
        /// </summary>
        /// <param name="displayName"> The new display name. </param>
        public async Task ChangeDisplayNameAsync(string displayName)
        {
            var idToken = await GetIdTokenAsync().ConfigureAwait(false);
            var result = await setAccountInfo.ExecuteAsync(new SetAccountDisplayName
            {
                IdToken = idToken,
                DisplayName = displayName,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            Info.DisplayName = result.DisplayName;

            await config.UserManager.UpdateExistingUserAsync(this).ConfigureAwait(false);
        }

        /// <summary>
        /// Link this user with another credential. The user represented by the <paramref name="credential"/> object must not already exist in Firebase.
        /// </summary>
        /// <param name="credential"> Provider specific credentials. </param>
        public async Task<FirebaseUser> LinkWithCredentialAsync(AuthCredential credential)
        {
            var provider = config.AuthProviders.FirstOrDefault(p => p.ProviderType == credential.ProviderType);

            if (provider == null)
            {
                throw new InvalidOperationException($"Provider {credential.ProviderType} is not configured");
            }

            var idToken = await GetIdTokenAsync().ConfigureAwait(false);
            var result = await provider.LinkWithCredentialAsync(idToken, credential).ConfigureAwait(false);

            Credential = result.Credential;
            Info = result.Info;

            await config.UserManager.UpdateExistingUserAsync(result).ConfigureAwait(false);

            return result;
        }

        public async Task<FirebaseUser> LinkWithRedirectAsync(FirebaseProviderType providerType, SignInRedirectDelegate redirectDelegate)
        {
            var provider = config.AuthProviders.FirstOrDefault(p => p.ProviderType == providerType);

            if (!(provider is OAuthProvider oauthProvider))
            {
                throw new InvalidOperationException("You cannot sign in with this provider using this method.");
            }

            var continuation = await oauthProvider.SignInAsync();
            var redirectUri = await redirectDelegate(continuation.Uri).ConfigureAwait(false);

            if (string.IsNullOrEmpty(redirectUri))
            {
                return null;
            }

            var idToken = await GetIdTokenAsync().ConfigureAwait(false);
            var result = await continuation.ContinueSignInAsync(redirectUri, idToken).ConfigureAwait(false);

            Credential = result.Credential;
            Info = result.Info;

            await config.UserManager.UpdateExistingUserAsync(result).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Unlinks a provider from a user account.
        /// </summary>
        public async Task<FirebaseUser> UnlinkAsync(FirebaseProviderType providerType)
        {
            var idToken = await GetIdTokenAsync().ConfigureAwait(false);
            await unlinkAccount.ExecuteAsync(new SetAccountUnlinkRequest
            {
                IdToken = idToken,
                DeleteProviders = new[]
                {
                    providerType
                }
            });

            return this;
        }

        /// <summary>
        /// Delete user account.
        /// </summary>
        public async Task DeleteAsync()
        {
            var tokenRequest = new IdTokenRequest
            {
                IdToken = await GetIdTokenAsync().ConfigureAwait(false)
            };
            await deleteAccount.ExecuteAsync(tokenRequest).ConfigureAwait(false);
            await config.UserManager.DeleteExistingUserAsync(Uid).ConfigureAwait(false);
        }
    }
}
