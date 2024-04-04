// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class ResetPasswordRequest
    {
        public ResetPasswordRequest(string email, string requestType = "PASSWORD_RESET")
        {
            this.email = email;
            this.requestType = requestType;
        }

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string requestType;

        public string RequestType => requestType;
    }
}
