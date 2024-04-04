// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class ProjectConfigResponse
    {
        [SerializeField]
        private string projectId;

        public string ProjectId => projectId;

        [SerializeField]
        private string[] authorizedDomains;

        public string[] AuthorizedDomains => authorizedDomains;
    }
}
