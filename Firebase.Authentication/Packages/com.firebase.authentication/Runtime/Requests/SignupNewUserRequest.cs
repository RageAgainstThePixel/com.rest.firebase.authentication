// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class SignupNewUserRequest : SecureTokenRequest
    {
        public SignupNewUserRequest(string email, string password, bool returnSecureToken)
            : base(returnSecureToken)
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
