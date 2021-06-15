namespace Firebase.Authentication.Requests
{
    internal class RefreshTokenResponse
    {
        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string IdToken { get; set; }

        public string UserId { get; set; }
    }
}
