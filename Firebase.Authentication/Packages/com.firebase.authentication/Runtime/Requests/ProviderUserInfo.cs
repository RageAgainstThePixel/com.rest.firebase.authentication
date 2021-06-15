namespace Firebase.Authentication.Requests
{
    internal class ProviderUserInfo
    {
        public FirebaseProviderType ProviderId { get; set; }

        public string DisplayName { get; set; }

        public string PhotoUrl { get; set; }

        public string FederatedId { get; set; }

        public string Email { get; set; }

        public string RawId { get; set; }
    }
}