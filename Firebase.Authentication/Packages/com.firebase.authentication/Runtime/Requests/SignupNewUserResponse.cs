// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class SignupNewUserResponse
    {
        public string IdToken { get; set; }

        public string Email { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }

        public string LocalId { get; set; }
    }
}
