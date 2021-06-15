using System.Collections.Generic;
using Newtonsoft.Json;

namespace Firebase.Authentication.Requests
{
    internal class CreateAuthUriRequest
    {
        public FirebaseProviderType? ProviderId { get; set; }

        public string ContinueUri { get; set; }

        [JsonProperty("customParameter")]
        public Dictionary<string, string> CustomParameters { get; set; }

        public string OauthScope { get; set; }

        public string Identifier { get; set; }
    }
}