namespace Firebase.Authentication.Requests
{
    public class UpdateAccountResponse
    {
        public string LocalId { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
