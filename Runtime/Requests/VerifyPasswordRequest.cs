// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class VerifyPasswordRequest
    {
        public VerifyPasswordRequest(string email, string password, bool returnSecureToken)
        {
            this.email = email;
            this.password = password;
            this.returnSecureToken = returnSecureToken;
        }

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string password;

        public string Password => password;

        [SerializeField]
        private bool returnSecureToken;

        public bool ReturnSecureToken => returnSecureToken;
    }
}
