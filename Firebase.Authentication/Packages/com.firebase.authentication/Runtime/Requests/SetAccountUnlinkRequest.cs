// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace Firebase.Authentication.Requests
{
    internal class SetAccountUnlinkRequest : IdTokenRequest
    {
        [JsonProperty("deleteProvider")]
        public FirebaseProviderType[] DeleteProviders { get; set; }
    }
}
