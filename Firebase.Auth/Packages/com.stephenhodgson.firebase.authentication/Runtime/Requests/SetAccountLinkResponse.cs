namespace Firebase.Authentication.Requests
{
    internal class SetAccountLinkResponse : SetAccountInfoResponse
    {
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
