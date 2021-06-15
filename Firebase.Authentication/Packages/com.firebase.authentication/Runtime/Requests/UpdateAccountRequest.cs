namespace Firebase.Authentication.Requests
{
    internal class UpdateAccountRequest : IdTokenRequest
    {
        public string Password { get; set; }

        public bool ReturnSecureToken { get; set; }
    }
}
