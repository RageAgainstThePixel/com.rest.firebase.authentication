// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class VerifyAssertionRequest : IdTokenRequest
    {
        /// <inheritdoc />
        public VerifyAssertionRequest(string idToken, string requestUri, string postBody, string pendingToken, string sessionId = null, bool returnIdpCredential = true, bool returnSecureToken = true)
            : base(idToken)
        {
            this.requestUri = requestUri;
            this.postBody = postBody;
            this.pendingToken = pendingToken;
            this.sessionId = sessionId;
            this.returnIdpCredential = returnIdpCredential;
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private string requestUri;

        public string RequestUri => requestUri;

        [SerializeField]
        private string postBody;

        public string PostBody => postBody;

        [SerializeField]
        private string pendingToken;

        public string PendingToken => pendingToken;

        [SerializeField]
        private string sessionId;

        public string SessionId => sessionId;

        [SerializeField]
        private bool returnIdpCredential;

        public bool ReturnIdpCredential => returnIdpCredential;

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
