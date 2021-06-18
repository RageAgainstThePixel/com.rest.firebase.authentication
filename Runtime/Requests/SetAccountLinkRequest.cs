// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class SetAccountLinkRequest : SetAccountInfoRequest
    {
        /// <inheritdoc />
        public SetAccountLinkRequest(string idToken, bool returnSecureToken, string email, string password)
            : base(idToken, returnSecureToken)
        {
            this.email = email;
            this.password = password;
        }

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string password;

        public string Password => password;
    }
}
