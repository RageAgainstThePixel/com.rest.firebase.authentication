// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;

namespace Firebase.Authentication.Requests
{
    internal class GetAccountInfoResponseUserInfo
    {
        public string LocalId { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string PhotoUrl { get; set; }

        public bool EmailVerified { get; set; }

        public ProviderUserInfo[] ProviderUserInfo { get; set; }

        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime ValidSince { get; set; }

        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime LastLoginAt { get; set; }

        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        public DateTime LastRefreshAt { get; set; }
    }
}
