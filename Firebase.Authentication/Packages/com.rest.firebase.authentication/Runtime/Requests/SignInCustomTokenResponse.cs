// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class SignInCustomTokenResponse
    {
        [SerializeField]
        private string idToken;

        public string IdToken => idToken;

        [SerializeField]
        private string refreshToken;

        public string RefreshToken => refreshToken;

        [SerializeField]
        private string expiresIn;

        public int ExpiresIn => int.Parse(expiresIn);
    }
}
