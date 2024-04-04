// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class SetAccountUnlinkRequest : IdTokenRequest
    {
        /// <inheritdoc />
        public SetAccountUnlinkRequest(string idToken, FirebaseProviderType[] deleteProviders)
            : base(idToken)
        {
            deleteProvider = deleteProviders;
        }

        [SerializeField]
        private FirebaseProviderType[] deleteProvider;

        public FirebaseProviderType[] DeleteProviders => deleteProvider;
    }
}
