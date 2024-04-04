// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class VerifyAssertionResponse
    {
        [SerializeField]
        private string federatedId;

        public string FederatedId => federatedId;

        [SerializeField]
        private FirebaseProviderType providerId;

        public FirebaseProviderType ProviderId => providerId;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private bool emailVerified;

        public bool EmailVerified => emailVerified;

        [SerializeField]
        private string firstName;

        public string FirstName => firstName;

        [SerializeField]
        private string fullName;

        public string FullName => fullName;

        [SerializeField]
        private string lastName;

        public string LastName => lastName;

        [SerializeField]
        private string photoUrl;

        public string PhotoUrl => photoUrl;

        [SerializeField]
        private string localId;

        public string LocalId => localId;

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private string idToken;

        public string IdToken => idToken;

        [SerializeField]
        private string context;

        public string Context => context;

        [SerializeField]
        private string oauthAccessToken;

        public string OauthAccessToken => oauthAccessToken;

        [SerializeField]
        private string oauthTokenSecret;

        public string OauthTokenSecret => oauthTokenSecret;

        [SerializeField]
        private int oauthExpireIn;

        public int OauthExpireIn => oauthExpireIn;

        [SerializeField]
        private string refreshToken;

        public string RefreshToken => refreshToken;

        [SerializeField]
        private int expiresIn;

        public int ExpiresIn => expiresIn;

        [SerializeField]
        private string oauthIdToken;

        public string OauthIdToken => oauthIdToken;

        [SerializeField]
        private string pendingToken;

        public string PendingToken => pendingToken;

        [SerializeField]
        private bool needConfirmation;

        public bool NeedConfirmation => needConfirmation;

        [SerializeField]
        private FirebaseProviderType[] verifiedProvider;

        public FirebaseProviderType[] VerifiedProviders => verifiedProvider;

        [SerializeField]
        private string errorMessage;

        public string ErrorMessage => errorMessage;

        public void Validate(AuthCredential credential)
            => VerifyAssertion.ValidateAssertionResponse(this, credential);
    }
}
