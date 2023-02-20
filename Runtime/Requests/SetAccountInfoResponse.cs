// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class SetAccountInfoResponse
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
        private ProviderUserInfo[] providerUserInfo;

        public ProviderUserInfo[] ProviderUserInfo => providerUserInfo;

        [SerializeField]
        private string passwordHash;

        public string PasswordHash => passwordHash;

        [SerializeField]
        private bool emailVerified;

        public bool EmailVerified => emailVerified;
    }
}
