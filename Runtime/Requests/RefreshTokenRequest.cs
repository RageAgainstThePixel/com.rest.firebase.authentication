// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class RefreshTokenRequest
    {
        public RefreshTokenRequest(string grantType, string refreshToken)
        {
            grant_type = grantType;
            refresh_token = refreshToken;
        }

        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private string grant_type;

        public string GrantType => grant_type;

        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private string refresh_token;

        public string RefreshToken => refresh_token;
    }
}
