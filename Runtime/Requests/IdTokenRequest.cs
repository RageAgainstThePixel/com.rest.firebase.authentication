// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class IdTokenRequest
    {
        public IdTokenRequest(string idToken)
        {
            this.idToken = idToken;
        }

        [SerializeField]
        private string idToken;

        public string IdToken => idToken;
    }
}
