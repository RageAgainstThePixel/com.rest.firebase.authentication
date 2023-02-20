// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class UpdateAccountRequest : IdTokenRequest
    {
        /// <inheritdoc />
        public UpdateAccountRequest(string idToken, string password, bool returnSecureToken)
            : base(idToken)
        {
            this.password = password;
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private string password;

        public string Password => password;

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
