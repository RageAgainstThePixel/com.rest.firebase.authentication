// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Net.Http;

namespace Firebase.Authentication.Requests
{
    internal class ProjectConfiguration : FirebaseRequestBase<object, ProjectConfigResponse>
    {
        public ProjectConfiguration(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleProjectConfigUrl;

        protected override HttpMethod Method => HttpMethod.Get;
    }
}
