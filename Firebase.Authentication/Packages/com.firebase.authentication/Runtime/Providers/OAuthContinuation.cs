using Firebase.Authentication.Requests;
using System;
using System.Threading.Tasks;

namespace Firebase.Authentication.Providers
{
    /// <summary>
    /// Continuation of OAuth sign in. This class processes the redirect uri user is navigated to and signs the user in.
    /// </summary>
    internal class OAuthContinuation
    {
        private readonly VerifyAssertion verifyAssertion;
        private readonly FirebaseConfiguration config;
        private readonly string sessionId;
        private readonly FirebaseProviderType providerType;

        internal OAuthContinuation(FirebaseConfiguration config, string uri, string sessionId, FirebaseProviderType providerType)
        {
            verifyAssertion = new VerifyAssertion(config);
            this.config = config;
            Uri = uri;
            this.sessionId = sessionId;
            this.providerType = providerType;
        }

        /// <summary>
        /// The uri user should be initially navigated to in browser.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Finishes OAuth sign in after user signs in in browser.
        /// </summary>
        /// <param name="redirectUri"> Final uri that user lands on after completing sign in in browser.</param>
        /// <param name="idToken"> Optional id token  of an existing Firebase user. If set, it will effectively perform account linking.</param>
        /// <returns></returns>
        public async Task<FirebaseUser> ContinueSignInAsync(string redirectUri, string idToken = null)
        {
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(
                providerType,
                new VerifyAssertionRequest
                {
                    IdToken = idToken,
                    RequestUri = redirectUri,
                    SessionId = sessionId,
                    ReturnIdpCredential = true,
                    ReturnSecureToken = true
                }).ConfigureAwait(false);
            var provider = config.GetAuthProvider(providerType) as OAuthProvider ??
                           throw new InvalidOperationException($"{providerType} is not a OAuthProvider");
            var credential = provider.GetCredential(response);
            response.Validate(credential);
            return user;
        }
    }
}
