// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class UpdateAccountRequest : IdTokenRequest
    {
        public string Password { get; set; }

        public bool ReturnSecureToken { get; set; }
    }
}
