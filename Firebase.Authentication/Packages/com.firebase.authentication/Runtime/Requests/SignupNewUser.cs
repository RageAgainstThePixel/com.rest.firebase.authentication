namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Creates a new user account.
    /// </summary>
    internal class SignupNewUser : FirebaseRequestBase<SignupNewUserRequest, SignupNewUserResponse>
    {
        public SignupNewUser(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSignUpUrl;
    }
}
