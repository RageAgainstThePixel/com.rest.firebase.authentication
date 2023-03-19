// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using Firebase.Authentication.Providers;
using Firebase.Authentication.CredentialStore;
using Firebase.Authentication.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Firebase.Authentication
{
    /// <summary>
    /// A Firebase client which encapsulates authenticated communication with Firebase servers.
    /// </summary>
    public class FirebaseAuthenticationClient
    {
        internal readonly FirebaseConfiguration Configuration;
        private readonly ProjectConfiguration projectConfiguration;
        private readonly SignupNewUser signupNewUser;
        private readonly AccountInfoUri accountsUri;

        private bool domainChecked;

        public FirebaseAuthenticationClient(FirebaseAuthentication authentication = null, FirebaseAuthProvider[] providers = null, AbstractUserCredentialStore userCredentialStore = null)
        {
            Configuration = new FirebaseConfiguration(this, authentication, providers, userCredentialStore);
            projectConfiguration = new ProjectConfiguration(Configuration);
            signupNewUser = new SignupNewUser(Configuration);
            accountsUri = new AccountInfoUri(Configuration);

            foreach (var provider in Configuration.AuthProviders)
            {
                provider.Initialize(Configuration);
            }

            Configuration.UserManager.UserChanged += TriggerAuthStateChanged;
        }

        ~FirebaseAuthenticationClient()
        {
            Configuration.UserManager.UserChanged -= TriggerAuthStateChanged;
        }

        private FirebaseUser loggedInUser;

        /// <summary>
        /// Currently signed in user.
        /// </summary>
        public FirebaseUser User
        {
            get
            {
                if (loggedInUser == null)
                {
                    throw new FirebaseAuthException("No user currently logged in", AuthErrorReason.UserNotFound);
                }

                return loggedInUser;
            }
            private set => loggedInUser = value;
        }

        /// <summary>
        /// Is there a user currently logged in?
        /// </summary>
        public bool IsUserLoggedIn => loggedInUser != null;

        private event Action<FirebaseUser> StateChanged;

        /// <summary>
        /// Event raised when the user auth state change changes. This can happen during sign in / sign out or when credential tokens change.
        /// </summary>
        public event Action<FirebaseUser> AuthenticatedStateChanged
        {
            add
            {
                StateChanged += value;

                // for every new listener trigger the AuthenticatedStateChanged event
                TriggerAuthStateChanged(Configuration.UserManager.GetUser);
            }
            remove => StateChanged -= value;
        }

        /// <summary>
        /// Signs in via third party OAuth providers - e.g. Google, Facebook etc.
        /// </summary>
        /// <param name="oauthProvider"><see cref="OAuthProvider"/> to sign in with.</param>
        /// <param name="redirectDelegate"> Delegate which should invoke the passed uri for oauth authentication and return the final redirect uri. </param>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> SignInWithRedirectAsync(OAuthProvider oauthProvider, SignInRedirectDelegate redirectDelegate)
        {
            if (oauthProvider is null)
            {
                throw new ArgumentException($"{nameof(oauthProvider)} cannot be null.");
            }

            await CheckAuthDomain().ConfigureAwait(false);

            var continuation = await oauthProvider.SignInAsync().ConfigureAwait(false);
            var redirectUri = await redirectDelegate(continuation.Uri).ConfigureAwait(false);

            if (string.IsNullOrEmpty(redirectUri))
            {
                return null;
            }

            var user = await continuation.ContinueSignInAsync(redirectUri).ConfigureAwait(false);
            await SaveTokenAsync(user).ConfigureAwait(false);
            return user;
        }

        /// <summary>
        /// Sign in with platform specific credential. For example:
        /// <code>
        /// var credential = GoogleProvider.GetCredential("token");
        /// </code>
        /// </summary>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var user = await Configuration
                .GetAuthProvider(credential.ProviderType)
                .SignInWithCredentialAsync(credential);
            await SaveTokenAsync(user).ConfigureAwait(false);
            return user;
        }

        /// <summary>
        /// Signs in as an anonymous user.
        /// </summary>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> SignInAnonymouslyAsync()
        {
            var response = await signupNewUser.ExecuteAsync(new SecureTokenRequest(true)).ConfigureAwait(false);
            var credential = new FirebaseCredential(response.IdToken, response.RefreshToken, response.ExpiresIn, FirebaseProviderType.Anonymous);
            var info = new UserInfo(response);
            var user = new FirebaseUser(Configuration, info, credential);
            await SaveTokenAsync(user).ConfigureAwait(false);
            return user;
        }

        /// <summary>
        /// Gets a list of sign-in methods for given email. If there are no methods, it means the user with given email doesn't exist.
        /// </summary>
        /// <returns><see cref="FetchUserProvidersResult"/></returns>
        public async Task<FetchUserProvidersResult> FetchSignInMethodsForEmailAsync(string email)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var request = new CreateAuthUriRequest(email, Configuration.RedirectUri);
            var response = await accountsUri.ExecuteAsync(request).ConfigureAwait(false);
            return new FetchUserProvidersResult(email, response.Registered, response.SigninMethods, response.AllProviders);
        }

        /// <summary>
        /// Signs in with email and password. If the email &amp; password combination is incorrect, <see cref="FirebaseAuthException"/> is thrown.
        /// </summary>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var provider = (EmailProvider)Configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            var result = await provider.SignInUserAsync(email, password).ConfigureAwait(false);
            await SaveTokenAsync(result).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Creates a new user with given email, password and display name (optional) and signs this user in.
        /// </summary>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> CreateUserWithEmailAndPasswordAsync(string email, string password, string displayName = null)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var provider = (EmailProvider)Configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            var result = await provider.SignUpUserAsync(email, password, displayName).ConfigureAwait(false);
            await SaveTokenAsync(result).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Sends a password reset email to given address.
        /// </summary>
        public async Task ResetEmailPasswordAsync(string email)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var provider = (EmailProvider)Configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            await provider.ResetEmailPasswordAsync(email).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs in using a custom token.
        /// </summary>
        /// <param name="customToken"></param>
        /// <returns><see cref="FirebaseUser"/></returns>
        public async Task<FirebaseUser> SignInWithCustomTokenAsync(string customToken)
        {
            await CheckAuthDomain().ConfigureAwait(false);
            var provider = (CustomTokenProvider)Configuration.GetAuthProvider(FirebaseProviderType.CustomToken);
            var result = await provider.SignInWithCustomTokenAsync(customToken).ConfigureAwait(false);
            await SaveTokenAsync(result).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Signs current user out.
        /// </summary>
        public async void SignOut()
        {
            try
            {
                await SignOutAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Signs current user out.
        /// </summary>
        public async Task SignOutAsync()
        {
            var uid = User?.Uid;
            await Configuration.UserManager.DeleteExistingUserAsync(uid).ConfigureAwait(false);
        }

        private void TriggerAuthStateChanged(FirebaseUser newUser)
        {
            User = newUser;
            StateChanged?.Invoke(newUser);
        }

        private async Task SaveTokenAsync(FirebaseUser newUser)
            => await Configuration.UserManager.SaveNewUserAsync(newUser).ConfigureAwait(false);

        private async Task CheckAuthDomain()
        {
            if (domainChecked) { return; }

            var result = await projectConfiguration.ExecuteAsync(null).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(result.ProjectId))
            {
                throw new FirebaseAuthException("Failed to get a valid project configuration from Google!", AuthErrorReason.Unknown);
            }

            if (result.AuthorizedDomains == null)
            {
                throw new FirebaseAuthException($"Failed to find valid domains for {result.ProjectId}", AuthErrorReason.Undefined);
            }

            if (!result.AuthorizedDomains.Contains(Configuration.AuthDomain))
            {
                throw new FirebaseAuthException($"{Configuration.AuthDomain} is not among the authorized domains:\n{string.Join(",\n", result.AuthorizedDomains)}", AuthErrorReason.Undefined);
            }

            domainChecked = true;
        }
    }
}
