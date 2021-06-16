// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class VerifyAssertionRequest : IdTokenRequest
    {
        public string RequestUri { get; set; }

        public string PostBody { get; set; }

        public string PendingToken { get; set; }

        public string SessionId { get; set; }

        public bool ReturnIdpCredential { get; set; }

        public bool ReturnSecureToken { get; set; }
    }
}
