// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    internal static class Endpoints
    {
        public const string GoogleRefreshAuth            = "https://securetoken.googleapis.com/v1/token?key={0}";

        public const string GoogleSignUpUrl              = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={0}";
        public const string GoogleAccountsUpdateUrl      = "https://identitytoolkit.googleapis.com/v1/accounts:update?key={0}";
        public const string GoogleAccountsCreateAuthUrl  = "https://identitytoolkit.googleapis.com/v1/accounts:createAuthUri?key={0}";
        public const string GooglePasswordUrl            = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={0}";

        public const string GoogleGetUser                = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo?key={0}";
        public const string GoogleIdentityUrl            = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyAssertion?key={0}";
        public const string GoogleDeleteUserUrl          = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/deleteAccount?key={0}";
        public const string GoogleGetConfirmationCodeUrl = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key={0}";
        public const string GoogleSetAccountUrl          = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/setAccountInfo?key={0}";
        public const string GoogleProjectConfigUrl       = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getProjectConfig?key={0}";
    }
}
