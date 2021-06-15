using Newtonsoft.Json;

namespace Firebase.Authentication.Requests
{
    public class VerifyAssertionResponse
    {
        public string FederatedId { get; set; }

        public FirebaseProviderType ProviderId { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }

        public string FirstName { get; set; }

        public string FullName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public string LocalId { get; set; }

        public string DisplayName { get; set; }

        public string IdToken { get; set; }

        public string Context { get; set; }

        public string OauthAccessToken { get; set; }

        public string OauthTokenSecret { get; set; }

        public int OauthExpireIn { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }

        public string OauthIdToken { get; set; }

        public string PendingToken { get; set; }

        public bool NeedConfirmation { get; set; }

        [JsonProperty("verifiedProvider")]
        public FirebaseProviderType[] VerifiedProviders { get; set; }

        public string ErrorMessage { get; set; }

        public void Validate(AuthCredential credential)
            => VerifyAssertion.ValidateAssertionResponse(this, credential);
    }
}
