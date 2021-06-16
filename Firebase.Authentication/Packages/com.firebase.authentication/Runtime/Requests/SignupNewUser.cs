// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Creates a new user account.
    /// </summary>
    internal class SignupNewUser : FirebaseRequestBase<SignupNewUserRequest, SignupNewUserResponse>
    {
        public SignupNewUser(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSignUpUrl;
    }
}
