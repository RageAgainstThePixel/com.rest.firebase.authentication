// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Rest.Authentication.Requests
{
    internal static class Endpoints
    {
        public const string GoogleRefreshAuth            = "https://securetoken.googleapis.com/v1/token?key={0}";

        public const string GoogleSignUpUrl              = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={0}";
        public const string GoogleAccountsUpdateUrl      = "https://identitytoolkit.googleapis.com/v1/accounts:update?key={0}";
        public const string GoogleAccountsCreateAuthUrl  = "https://identitytoolkit.googleapis.com/v1/accounts:createAuthUri?key={0}";
        public const string GooglePasswordUrl            = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={0}";
        public const string GoogleGetUserUrl             = "https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={0}";
        public const string GoogleIdentityUrl            = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={0}";
        public const string GoogleDeleteUserUrl          = "https://identitytoolkit.googleapis.com/v1/accounts:delete?key={0}";
        public const string GoogleGetConfirmationCodeUrl = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={0}";
        public const string GoogleSignInWithCustomToken  = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key={0}";

        public const string GoogleProjectConfigUrl         = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getProjectConfig?key={0}";
        public const string GoogleProjectVerifyPhoneNumber = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPhoneNumber?key={0}";
    }
}
