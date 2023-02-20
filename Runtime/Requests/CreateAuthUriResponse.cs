// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class CreateAuthUriResponse
    {
        [SerializeField]
        private string authUri;

        public string AuthUri => authUri;

        [SerializeField]
        private string providerId;

        public FirebaseProviderType ProviderId
        {
            get
            {
                if (Enum.TryParse<FirebaseProviderType>(providerId, true, out var result))
                {
                    return result;
                }

                throw new Exception($"Failed to parse {providerId} to {nameof(FirebaseProviderType)}");
            }
        }

        [SerializeField]
        private string sessionId;

        public string SessionId => sessionId;

        [SerializeField]
        private bool registered;

        public bool Registered => registered;

        [SerializeField]
        private List<FirebaseProviderType> signinMethods;

        public List<FirebaseProviderType> SigninMethods => signinMethods;

        [SerializeField]
        private List<FirebaseProviderType> allProviders;

        public List<FirebaseProviderType> AllProviders => allProviders;
    }
}
