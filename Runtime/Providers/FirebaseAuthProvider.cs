using System.Threading.Tasks;
using Firebase.Authentication.Requests;

namespace Firebase.Authentication.Providers
{
    /// <summary>
    /// Base class for Firebase auth providers.
    /// </summary>
    public abstract class FirebaseAuthProvider
    {
        private CreateAuthUri createAuthUri;
        internal FirebaseConfiguration Configuration;

        public abstract FirebaseProviderType ProviderType { get; }

        internal virtual void Initialize(FirebaseConfiguration configuration)
        {
            Configuration = configuration;
            createAuthUri = new CreateAuthUri(configuration);
        }

        protected internal abstract Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential);

        protected internal abstract Task<FirebaseUser> LinkWithCredentialAsync(string idToken, AuthCredential credential);

        internal async Task<CreateAuthUriResponse> SendAuthRequest(CreateAuthUriRequest request)
        {
            return await createAuthUri.ExecuteAsync(request);
        }
    }
}
