// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal class VerifyPassword : FirebaseRequestBase<VerifyPasswordRequest, VerifyPasswordResponse>
    {
        public VerifyPassword(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GooglePasswordUrl;
    }
}
