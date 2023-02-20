// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class ProviderUserInfo
    {
        [SerializeField]
        private FirebaseProviderType providerId;

        public FirebaseProviderType ProviderId => providerId;

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private string photoUrl;

        public string PhotoUrl => photoUrl;

        [SerializeField]
        private string federatedId;

        public string FederatedId => federatedId;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private string rawId;

        public string RawId => rawId;
    }
}
