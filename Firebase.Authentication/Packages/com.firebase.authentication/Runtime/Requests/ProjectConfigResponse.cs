namespace Firebase.Authentication.Requests
{
    internal class ProjectConfigResponse
    {
        public string ProjectId { get; set; }

        public string[] AuthorizedDomains { get; set; }
    }
}