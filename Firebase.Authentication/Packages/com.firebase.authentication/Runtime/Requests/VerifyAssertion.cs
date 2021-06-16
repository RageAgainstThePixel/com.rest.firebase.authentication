// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Exceptions;
using System.Threading.Tasks;

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Finishes oauth authentication processing.
    /// </summary>
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
            var assertion = await ExecuteAsync(request).ConfigureAwait(false);

            var userInfo = new UserInfo
            {
                DisplayName = assertion.DisplayName,
                FirstName = assertion.FirstName,
                LastName = assertion.LastName,
                Email = assertion.Email,
                IsEmailVerified = assertion.EmailVerified,
                FederatedId = assertion.FederatedId,
                Uid = assertion.LocalId,
                PhotoUrl = assertion.PhotoUrl,
                IsAnonymous = false
            };

            var token = new FirebaseCredential
            {
                ExpiresIn = assertion.ExpiresIn,
                RefreshToken = assertion.RefreshToken,
                IdToken = assertion.IdToken,
                ProviderType = providerType
            };

            return (new FirebaseUser(Configuration, userInfo, token), assertion);
        }

        protected override string UrlFormat => Endpoints.GoogleIdentityUrl;
    }
}
