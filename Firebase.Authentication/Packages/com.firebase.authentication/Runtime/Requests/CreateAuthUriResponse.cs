using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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