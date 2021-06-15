namespace Firebase.Authentication.Requests
{
    internal abstract class SetAccountInfoRequest : IdTokenRequest
    {
        public bool ReturnSecureToken { get; set; }
    }
}