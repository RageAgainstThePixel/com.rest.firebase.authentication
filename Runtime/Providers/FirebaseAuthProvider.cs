// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using Firebase.Authentication.Requests;

namespace Firebase.Authentication.Providers
{
    /// <summary>
    /// Base class for Firebase auth providers.
    /// </summary>
    public abstract class FirebaseAuthProvider
    {
        private AccountInfoUri accountInfoUri;
        internal FirebaseConfiguration Configuration;

        /// <summary>
        /// The <see cref="FirebaseProviderType"/> for this provider instance.
        /// </summary>
        public abstract FirebaseProviderType ProviderType { get; }

        internal virtual void Initialize(FirebaseConfiguration configuration)
        {
            Configuration = configuration;
            accountInfoUri = new AccountInfoUri(configuration);
        }

        protected internal abstract Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential);

        protected internal abstract Task<FirebaseUser> LinkWithCredentialAsync(string idToken, AuthCredential credential);

        internal async Task<CreateAuthUriResponse> SendAuthRequest(CreateOAuthUriRequest request)
            => await accountInfoUri.ExecuteAsync(request);
    }
}
