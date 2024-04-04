// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal abstract class SetAccountInfoRequest : IdTokenRequest
    {
        /// <inheritdoc />
        protected SetAccountInfoRequest(string idToken, bool returnSecureToken)
            : base(idToken)
        {
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
