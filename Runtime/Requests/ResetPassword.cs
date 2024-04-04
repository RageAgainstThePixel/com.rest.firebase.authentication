// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Rest.Authentication.Requests
{
    internal class ResetPassword : FirebaseRequestBase<ResetPasswordRequest, ResetPasswordResponse>
    {
        public ResetPassword(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleGetConfirmationCodeUrl;
    }
}
