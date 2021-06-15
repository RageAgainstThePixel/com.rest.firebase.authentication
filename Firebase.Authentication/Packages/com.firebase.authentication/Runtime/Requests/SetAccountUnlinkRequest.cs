using Newtonsoft.Json;

namespace Firebase.Authentication.Requests
{
    internal class SetAccountUnlinkRequest : IdTokenRequest
    {
        [JsonProperty("deleteProvider")]
        public FirebaseProviderType[] DeleteProviders { get; set; }
    }
}
