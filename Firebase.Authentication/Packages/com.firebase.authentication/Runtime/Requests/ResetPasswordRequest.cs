namespace Firebase.Authentication.Requests
{
    internal class ResetPasswordRequest
    {
        public string Email { get; set; }

        public string RequestType { get; set; } = "PASSWORD_RESET";
    }
}
