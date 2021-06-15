namespace Firebase.Authentication.Requests
{
    internal class SetAccountLinkRequest : SetAccountInfoRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}