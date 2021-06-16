// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Refreshes IdToken using a refresh token.
    /// </summary>
    internal class RefreshToken : FirebaseRequestBase<RefreshTokenRequest, RefreshTokenResponse>
    {
        public RefreshToken(FirebaseConfiguration configuration)
            : base(configuration)
        {
            JsonSettingsOverride = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        protected override JsonSerializerSettings JsonSettingsOverride { get; }

        protected override string UrlFormat => Endpoints.GoogleRefreshAuth;
    }
}
