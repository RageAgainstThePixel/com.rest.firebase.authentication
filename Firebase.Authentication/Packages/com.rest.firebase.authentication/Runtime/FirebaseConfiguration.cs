// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Net.Http;
using Firebase.Rest.Authentication.CredentialStore;
using Firebase.Rest.Authentication.Providers;

namespace Firebase.Rest.Authentication
{
    internal class FirebaseConfiguration
    {
        public FirebaseConfiguration(FirebaseAuthenticationClient client, FirebaseAuthentication authentication = null, FirebaseAuthProvider[] authProviders = null, AbstractUserCredentialStore userCredentialStore = null)
        {
            Client = client;
            this.authentication = authentication ?? FirebaseAuthentication.Default;

            if (authProviders is not { Length: not 0 })
            {
                authProviders = new FirebaseAuthProvider[]
                {
                    new EmailProvider(),
                    new CustomTokenProvider(),
                    new AppleProvider(),
                    new FacebookProvider(),
                    new GithubProvider(),
                    new GoogleProvider(),
                    new MicrosoftProvider(),
                    new TwitterProvider(),
                };
            }

            AuthProviders = authProviders;
            HttpClient = new HttpClient();
            userCredentialStore ??= new PlayerPreferencesCredentialStore();
            userCredentialStore.Configuration = this;
            UserManager = new UserManager(userCredentialStore);
            RedirectUri = $"https://{AuthDomain}/__/auth/handler";
        }

        private readonly FirebaseAuthentication authentication;

        public string ProjectId => authentication.ProjectId;

        public string ApiKey => authentication.ApiKey;

        public string AuthDomain => authentication.AuthDomain;

        public FirebaseAuthProvider[] AuthProviders { get; }

        internal string RedirectUri { get; }

        internal HttpClient HttpClient { get; }

        internal UserManager UserManager { get; }

        internal FirebaseAuthProvider GetAuthProvider(FirebaseProviderType providerType)
            => AuthProviders.FirstOrDefault(authProvider => authProvider.ProviderType == providerType) ??
               throw new InvalidOperationException($"{nameof(FirebaseProviderType)}.{providerType} is not configured.");

        internal FirebaseAuthenticationClient Client { get; }
    }
}
