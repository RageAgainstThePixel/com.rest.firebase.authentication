// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class RefreshTokenRequest
    {
        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }
}
