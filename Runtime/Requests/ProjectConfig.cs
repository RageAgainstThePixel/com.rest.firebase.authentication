using System.Net.Http;

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Get basic config info about the firebase project.
    /// </summary>
    internal class ProjectConfig : FirebaseRequestBase<object, ProjectConfigResponse>
    {
        public ProjectConfig(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleProjectConfigUrl;

        protected override HttpMethod Method => HttpMethod.Get;
    }
}
