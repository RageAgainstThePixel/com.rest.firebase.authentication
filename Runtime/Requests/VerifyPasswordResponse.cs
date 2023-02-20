// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class VerifyPasswordResponse
    {
        [SerializeField]
        private string localId;

        public string LocalId => localId;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private string idToken;

        public string IdToken => idToken;

        [SerializeField]
        private bool registered;

        public bool Registered => registered;

        [SerializeField]
        private string refreshToken;

        public string RefreshToken => refreshToken;

        [SerializeField]
        private int expiresIn;

        public int ExpiresIn => expiresIn;
    }
}
