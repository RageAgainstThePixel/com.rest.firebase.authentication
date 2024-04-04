// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    public class UpdateAccountResponse
    {
        [SerializeField]
        private string localId;

        public string LocalId => localId;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string passwordHash;

        public string PasswordHash => passwordHash;

        [SerializeField]
        private string idToken;

        public string IdToken => idToken;

        [SerializeField]
        private string refreshToken;

        public string RefreshToken => refreshToken;

        [SerializeField]
        private int expiresIn;

        public int ExpiresIn => expiresIn;
    }
}
