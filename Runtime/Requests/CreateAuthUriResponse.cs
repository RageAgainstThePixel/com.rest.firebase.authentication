// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Firebase.Authentication.Requests
{
    internal class CreateAuthUriResponse
    {
        public string AuthUri { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FirebaseProviderType ProviderId { get; set; }

        public string SessionId { get; set; }

        public bool Registered { get; set; }

        public List<FirebaseProviderType> SigninMethods { get; set; }

        public List<FirebaseProviderType> AllProviders { get; set; }
    }
}
