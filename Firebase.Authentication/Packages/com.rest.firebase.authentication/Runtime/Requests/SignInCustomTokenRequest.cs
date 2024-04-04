// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class SignInCustomTokenRequest
    {
        public SignInCustomTokenRequest(string token, bool returnSecureToken = true)
        {
            this.token = token;
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private string token;

        public string Token => token;

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
