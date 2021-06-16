// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class ProjectConfigResponse
    {
        public string ProjectId { get; set; }

        public string[] AuthorizedDomains { get; set; }
    }
}
