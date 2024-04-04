// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class CreateOAuthUriRequest : CreateAuthUriRequest
    {
        public CreateOAuthUriRequest(FirebaseProviderType? providerId, string continueUri, Dictionary<string, string> customParameters, string oauthScope, string identifier) : base(identifier, continueUri)
        {
            if (providerId.HasValue)
            {
                this.providerId = providerId.Value.ToString();
            }

            this.customParameter = customParameters;
            this.oauthScope = oauthScope;
        }

        [SerializeField]
        private string providerId;

        public string ProviderId => providerId;

        private Dictionary<string, string> customParameter;

        public Dictionary<string, string> CustomParameters => customParameter;

        [SerializeField]
        private string oauthScope;

        public string OauthScope => oauthScope;
    }
}
