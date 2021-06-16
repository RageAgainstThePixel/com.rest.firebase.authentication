// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Net.Http;

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Get basic config info about the firebase project.
    /// </summary>
    internal class ProjectConfig : FirebaseRequestBase<object, ProjectConfigResponse>
    {
        public ProjectConfig(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleProjectConfigUrl;

        protected override HttpMethod Method => HttpMethod.Get;
    }
}
