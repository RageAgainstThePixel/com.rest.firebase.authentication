namespace Firebase.Authentication.Requests
{
    internal class RefreshTokenRequest
    {
        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }
}
