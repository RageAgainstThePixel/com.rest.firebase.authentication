// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class RefreshTokenResponse
    {
        [SerializeField]
        private int expires_in;

        public int ExpiresIn => expires_in;

        [SerializeField]
        private string refresh_token;

        public string RefreshToken => refresh_token;

        [SerializeField]
        private string id_token;

        public string IdToken => id_token;

        [SerializeField]
        private string user_id;

        public string UserId => user_id;
    }
}
