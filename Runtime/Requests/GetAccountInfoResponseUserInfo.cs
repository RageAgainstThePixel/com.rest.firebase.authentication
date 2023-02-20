// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class GetAccountInfoResponseUserInfo
    {
        [SerializeField]
        private string localId;

        public string LocalId => localId;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private bool emailVerified;

        public bool EmailVerified => emailVerified;

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private ProviderUserInfo[] providerUserInfo;

        public ProviderUserInfo[] ProviderUserInfo => providerUserInfo;

        [SerializeField]
        private string photoUrl;

        public string PhotoUrl => photoUrl;

        [SerializeField]
        private long validSince;

        public DateTime ValidSince => DateTimeOffset.FromUnixTimeSeconds(validSince).DateTime;

        [SerializeField]
        private long lastLoginAt;

        public DateTime LastLoginAt => DateTimeOffset.FromUnixTimeMilliseconds(lastLoginAt).DateTime;

        [SerializeField]
        private long createdAt;

        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeMilliseconds(createdAt).DateTime;

        [SerializeField]
        private string lastRefreshAt;

        public string LastRefreshAt => lastRefreshAt;

        [SerializeField]
        private bool customAuth;

        public bool CustomAuth => customAuth;
    }
}
