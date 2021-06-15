namespace Firebase.Authentication.Requests
{
    internal class SetAccountInfoResponse
    {
        public string LocalId { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public ProviderUserInfo[] ProviderUserInfo { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailVerified { get; set; }
    }
}
