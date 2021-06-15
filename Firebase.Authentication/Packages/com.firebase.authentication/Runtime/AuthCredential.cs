namespace Firebase.Authentication
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
