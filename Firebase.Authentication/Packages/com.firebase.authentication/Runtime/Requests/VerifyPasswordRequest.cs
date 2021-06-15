namespace Firebase.Authentication.Requests
{
    internal class VerifyPasswordRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool ReturnSecureToken { get; set; }
    }
}