// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class SecureTokenRequest
    {
        public SecureTokenRequest(bool returnSecureToken)
        {
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
