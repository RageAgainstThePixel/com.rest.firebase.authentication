// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal abstract class SetAccountInfoRequest : IdTokenRequest
    {
        public bool ReturnSecureToken { get; set; }
    }
}
