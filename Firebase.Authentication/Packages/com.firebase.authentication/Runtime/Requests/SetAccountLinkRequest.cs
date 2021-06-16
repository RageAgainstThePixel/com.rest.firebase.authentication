// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class SetAccountLinkRequest : SetAccountInfoRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
