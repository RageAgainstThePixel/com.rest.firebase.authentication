// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System.Collections.Generic;

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
