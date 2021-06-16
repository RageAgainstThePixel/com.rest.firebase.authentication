// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class RefreshTokenResponse
    {
        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string IdToken { get; set; }

        public string UserId { get; set; }
    }
}
