using System;
using UnityEngine;

namespace Firebase.Authentication.Requests
{
    [Serializable]
    internal class CreateAuthUriRequest
    {
        public CreateAuthUriRequest(string email, string continueUri)
        {
            this.identifier = email;
            this.continueUri = continueUri;
        }

        [SerializeField]
        private string identifier;

        public string Identifier => identifier;

        [SerializeField]
        private string continueUri;

        public string ContinueUri => continueUri;
    }
}
