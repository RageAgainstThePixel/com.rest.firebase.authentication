// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class SetAccountDisplayName : SetAccountInfoRequest
    {
        /// <inheritdoc />
        public SetAccountDisplayName(string idToken, bool returnSecureToken, string displayName)
            : base(idToken, returnSecureToken)
        {
            this.displayName = displayName;
        }

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;
    }
}
