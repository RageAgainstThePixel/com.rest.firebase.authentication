// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class CreateAuthUriRequest
    {
        public CreateAuthUriRequest(FirebaseProviderType? providerId, string continueUri, Dictionary<string, string> customParameters, string oauthScope, string identifier)
        {
            if (providerId.HasValue)
            {
                this.providerId = providerId.Value.ToString();
            }

            this.continueUri = continueUri;
            this.customParameter = customParameters;
            this.oauthScope = oauthScope;
            this.identifier = identifier;
        }

        [SerializeField]
        private string providerId;

        public string ProviderId => providerId;

        [SerializeField]
        private string continueUri;

        public string ContinueUri => continueUri;

        private Dictionary<string, string> customParameter;

        public Dictionary<string, string> CustomParameters => customParameter;

        [SerializeField]
        private string oauthScope;

        public string OauthScope => oauthScope;

        [SerializeField]
        private string identifier;

        public string Identifier => identifier;
    }
}
