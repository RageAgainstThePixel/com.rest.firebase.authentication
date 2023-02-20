// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class SignupNewUser : FirebaseRequestBase<SecureTokenRequest, SignupNewUserResponse>
    {
        public SignupNewUser(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleSignUpUrl;
    }
}
