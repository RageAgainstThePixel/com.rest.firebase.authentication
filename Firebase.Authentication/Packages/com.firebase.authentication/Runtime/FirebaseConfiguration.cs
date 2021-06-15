using Firebase.Authentication.Providers;
using Firebase.Authentication.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;

namespace Firebase.Authentication
{
    /// <summary>
    /// Configuration of firebase authentication.
    /// </summary>
    public class FirebaseConfiguration
    {
        public FirebaseConfiguration(string apiKey, string authDomain, FirebaseAuthProvider[] authProviders, string userCacheFolder = null)
        {

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"no {nameof(apiKey)} provided.");
            }

            if (string.IsNullOrWhiteSpace(authDomain))
            {
                throw new ArgumentException($"no {nameof(authDomain)} provided.");
            }

            if (authProviders == null || authProviders.Length == 0)
            {
                throw new ArgumentException($"no {nameof(authProviders)} provided.");
            }

            ApiKey = apiKey;
            AuthDomain = authDomain;
            AuthProviders = authProviders;
            HttpClient = new HttpClient();
            JsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            JsonSettings.Converters.Add(
                new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });
            UserManager = new UserManager(string.IsNullOrWhiteSpace(userCacheFolder)
                ? (IUserRepository)new InMemoryRepository()
                : (IUserRepository)new FileUserRepository(this, userCacheFolder));
            RedirectUri = $"https://{AuthDomain}/__/auth/handler";
        }

        /// <summary>
        /// The api key of your Firebase app.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Collection of auth providers (e.g. Google, Facebook etc.)
        /// </summary>
        public FirebaseAuthProvider[] AuthProviders { get; }

        /// <summary>
        /// Auth domain of your firebase app, e.g. hello.firebaseapp.com
        /// </summary>
        public string AuthDomain { get; }

        /// <summary>
        /// Specifies the uri that oauth provider will navigate to to finish auth.
        /// </summary>
        internal string RedirectUri { get; }

        internal JsonSerializerSettings JsonSettings { get; }

        internal HttpClient HttpClient { get; }

        internal UserManager UserManager { get; }

        /// <summary>
        /// Get provider instance for given <paramref name="providerType"/>.
        /// </summary>
        internal FirebaseAuthProvider GetAuthProvider(FirebaseProviderType providerType)
        {
            return AuthProviders.FirstOrDefault(authProvider => authProvider.ProviderType == providerType)
                ?? throw new InvalidOperationException($"{nameof(FirebaseProviderType)}.{providerType} is not configured, you need to add it to your {nameof(FirebaseConfiguration)}");
        }
    }
}
