// Copyright (c) Stephen Hodgson. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using Firebase.Authentication.Providers;
using Firebase.Authentication.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Firebase.Authentication
{
    /// <summary>
    /// Firebase client which encapsulates communication with Firebase servers.
    /// </summary>
    public class FirebaseAuthenticationClient
    {
        private readonly FirebaseConfiguration configuration;
        private readonly ProjectConfig projectConfig;
        private readonly SignupNewUser signupNewUser;
        private readonly CreateAuthUri createAuthUri;

        private bool domainChecked;

        public FirebaseAuthenticationClient(FirebaseAuthentication authentication = null, FirebaseAuthProvider[] providers = null, string userCacheDirectory = null)
        {
            configuration = new FirebaseConfiguration(authentication, providers, userCacheDirectory);
            projectConfig = new ProjectConfig(configuration);
            signupNewUser = new SignupNewUser(configuration);
            createAuthUri = new CreateAuthUri(configuration);

            foreach (var provider in configuration.AuthProviders)
            {
                provider.Initialize(configuration);
            }

            configuration.UserManager.UserChanged += TriggerAuthStateChanged;
        }

        ~FirebaseAuthenticationClient()
        {
            configuration.UserManager.UserChanged -= TriggerAuthStateChanged;
        }

        /// <summary>
        /// Currently signed in user.
        /// </summary>
        public FirebaseUser User { get; private set; }

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
                TriggerAuthStateChanged(configuration.UserManager.GetUser);
            }
            remove => StateChanged -= value;
        }

        /// <summary>
        /// Signs in via third party OAuth providers - e.g. Google, Facebook etc.
        /// </summary>
        /// <param name="oauthProvider"><see cref="OAuthProvider"/> to sign in with.</param>
        /// <param name="redirectDelegate"> Delegate which should invoke the passed uri for oauth authentication and return the final redirect uri. </param>
        public async Task<FirebaseUser> SignInWithRedirectAsync(OAuthProvider oauthProvider, SignInRedirectDelegate redirectDelegate)
        {
            if (oauthProvider is null)
            {
                throw new ArgumentException($"{nameof(oauthProvider)} cannot be null.");
            }

            await CheckAuthDomain();

            var continuation = await oauthProvider.SignInAsync();
            var redirectUri = await redirectDelegate(continuation.Uri).ConfigureAwait(false);

            if (string.IsNullOrEmpty(redirectUri))
            {
                return null;
            }

            var user = await continuation.ContinueSignInAsync(redirectUri).ConfigureAwait(false);

            SaveToken(user);

            return user;
        }

        /// <summary>
        /// Sign in with platform specific credential. For example:
        /// <code>
        /// var credential = GoogleProvider.GetCredential("token");
        /// </code>
        /// </summary>
        public async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            await CheckAuthDomain().ConfigureAwait(false);

            var user = await configuration
                .GetAuthProvider(credential.ProviderType)
                .SignInWithCredentialAsync(credential);

            SaveToken(user);
            return user;
        }

        /// <summary>
        /// Signs in as an anonymous user.
        /// </summary>
        public async Task<FirebaseUser> SignInAnonymouslyAsync()
        {
            var response = await signupNewUser.ExecuteAsync(new SignupNewUserRequest
            {
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            var credential = new FirebaseCredential
            {
                ExpiresIn = response.ExpiresIn,
                IdToken = response.IdToken,
                RefreshToken = response.RefreshToken,
                ProviderType = FirebaseProviderType.Anonymous
            };

            var info = new UserInfo
            {
                Uid = response.LocalId,
                IsAnonymous = true
            };

            var user = new FirebaseUser(configuration, info, credential);
            SaveToken(user);
            return user;
        }

        /// <summary>
        /// Gets a list of sign-in methods for given email. If there are no methods, it means the user with given email doesn't exist.
        /// </summary>
        public async Task<FetchUserProvidersResult> FetchSignInMethodsForEmailAsync(string email)
        {
            await CheckAuthDomain().ConfigureAwait(false);

            var request = new CreateAuthUriRequest
            {
                ContinueUri = configuration.RedirectUri,
                Identifier = email
            };

            var response = await createAuthUri.ExecuteAsync(request).ConfigureAwait(false);

            return new FetchUserProvidersResult(email, response.Registered, response.SigninMethods, response.AllProviders);
        }

        /// <summary>
        /// Signs in with email and password. If the email &amp; password combination is incorrect, <see cref="FirebaseAuthException"/> is thrown.
        /// </summary>
        public async Task<FirebaseUser> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            await CheckAuthDomain().ConfigureAwait(false);

            var provider = (EmailProvider)configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            var result = await provider.SignInUserAsync(email, password).ConfigureAwait(false);

            SaveToken(result);

            return result;
        }

        /// <summary>
        /// Creates a new user with given email, password and display name (optional) and signs this user in.
        /// </summary>
        public async Task<FirebaseUser> CreateUserWithEmailAndPasswordAsync(string email, string password, string displayName = null)
        {
            await CheckAuthDomain().ConfigureAwait(false);

            var provider = (EmailProvider)configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            var result = await provider.SignUpUserAsync(email, password, displayName).ConfigureAwait(false);

            SaveToken(result);

            return result;
        }

        /// <summary>
        /// Sends a password reset email to given address.
        /// </summary>
        public async Task ResetEmailPasswordAsync(string email)
        {
            await CheckAuthDomain().ConfigureAwait(false);

            var provider = (EmailProvider)configuration.GetAuthProvider(FirebaseProviderType.EmailAndPassword);
            await provider.ResetEmailPasswordAsync(email).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs current user out.
        /// </summary>
        public void SignOut()
        {
            var uid = User?.Uid;
            User = null;
            configuration.UserManager.DeleteExistingUser(uid);
        }

        private void TriggerAuthStateChanged(FirebaseUser user)
        {
            User = user;
            StateChanged?.Invoke(user);
        }

        private void SaveToken(FirebaseUser user)
            => configuration.UserManager.SaveNewUser(user);

        private async Task CheckAuthDomain()
        {
            if (domainChecked)
            {
                return;
            }

            var result = await projectConfig.ExecuteAsync(null).ConfigureAwait(false);

            if (!result.AuthorizedDomains.Contains(configuration.AuthDomain))
            {
                throw new InvalidOperationException("Auth domain is not among the authorized ones");
            }

            domainChecked = true;
        }
    }
}
