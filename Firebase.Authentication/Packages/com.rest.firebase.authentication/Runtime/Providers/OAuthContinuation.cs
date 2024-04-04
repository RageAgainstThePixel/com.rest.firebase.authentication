// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Firebase.Rest.Authentication.Requests;

namespace Firebase.Rest.Authentication.Providers
{
    /// <summary>
    /// Continuation of OAuth sign in. This class processes the redirect uri user is navigated to and signs the user in.
    /// </summary>
    internal class OAuthContinuation
    {
        private readonly string sessionId;
        private readonly VerifyAssertion verifyAssertion;
        private readonly FirebaseProviderType providerType;
        private readonly FirebaseConfiguration configuration;

        internal OAuthContinuation(FirebaseConfiguration configuration, string uri, string sessionId, FirebaseProviderType providerType)
        {
            verifyAssertion = new VerifyAssertion(configuration);
            this.configuration = configuration;
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
                new VerifyAssertionRequest(
                    idToken,
                    redirectUri,
                    postBody: null,
                    pendingToken: null,
                    sessionId))
                .ConfigureAwait(false);
            var provider = configuration.GetAuthProvider(providerType) as OAuthProvider ??
                           throw new InvalidOperationException($"{providerType} is not a OAuthProvider");
            var credential = provider.GetCredential(response);
            response.Validate(credential);
            return user;
        }
    }
}
