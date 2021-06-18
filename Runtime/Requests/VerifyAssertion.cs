// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using System.Threading.Tasks;

namespace Firebase.Authentication.Requests
{
    internal class VerifyAssertion : FirebaseRequestBase<VerifyAssertionRequest, VerifyAssertionResponse>
    {
        public VerifyAssertion(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        public static void ValidateAssertionResponse(VerifyAssertionResponse response, AuthCredential credential)
        {
            if (response.NeedConfirmation)
            {
                throw new FirebaseAuthLinkConflictException(
                    response.Email,
                    response.VerifiedProviders);
            }

            switch (response.ErrorMessage)
            {
                case "FEDERATED_USER_ID_ALREADY_LINKED":
                    throw new FirebaseAuthWithCredentialException("This credential is already associated with a different user account", credential, AuthErrorReason.AlreadyLinked);
                case "EMAIL_EXISTS":
                    // trying to link OAuth account to email which already exists
                    throw new FirebaseAuthWithCredentialException("This email is already associated with another account", response.Email, credential, AuthErrorReason.EmailExists);
            }
        }

        public async Task<(FirebaseUser, VerifyAssertionResponse)> ExecuteAndParseAsync(FirebaseProviderType providerType, VerifyAssertionRequest request)
        {
            var response = await ExecuteAsync(request).ConfigureAwait(false);

            return (new FirebaseUser(
                Configuration,
                new UserInfo(response),
                new FirebaseCredential(
                    response.IdToken,
                    response.RefreshToken,
                    response.ExpiresIn,
                providerType)),
                response);
        }

        protected override string UrlFormat => Endpoints.GoogleIdentityUrl;
    }
}
