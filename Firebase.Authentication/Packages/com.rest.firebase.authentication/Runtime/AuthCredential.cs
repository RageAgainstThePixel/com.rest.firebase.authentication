// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Rest.Authentication
{
    /// <summary>
    /// Base class for provider-specific AuthCredentials.
    /// </summary>
    public abstract class AuthCredential
    {
        protected AuthCredential(FirebaseProviderType providerType)
        {
            ProviderType = providerType;
        }

        public FirebaseProviderType ProviderType { get; }
    }
}
