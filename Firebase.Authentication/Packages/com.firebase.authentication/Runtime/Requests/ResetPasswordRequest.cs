// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class ResetPasswordRequest
    {
        public string Email { get; set; }

        public string RequestType { get; set; } = "PASSWORD_RESET";
    }
}
