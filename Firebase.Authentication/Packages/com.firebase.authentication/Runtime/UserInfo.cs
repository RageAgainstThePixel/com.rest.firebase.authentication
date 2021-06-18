// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Requests;
using System;
using System.Linq;
using UnityEngine;

namespace Firebase.Authentication
{
    /// <summary>
    /// Basic information about the signed in user.
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        public UserInfo(string uid, string federatedId, string firstName, string lastName, string displayName, string email, bool isEmailVerified, string photoUrl, bool isAnonymous)
        {
            this.uid = uid;
            this.federatedId = federatedId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.displayName = displayName;
            this.email = email;
            this.isEmailVerified = isEmailVerified;
            this.photoUrl = photoUrl;
            this.isAnonymous = isAnonymous;
        }

        internal UserInfo(SignupNewUserResponse info)
        {
            uid = info.LocalId;
            email = info.Email;
            isAnonymous = true;
        }

        internal UserInfo(SetAccountInfoResponse info)
        {
            uid = info.LocalId;
            displayName = info.DisplayName;
            email = info.Email;
            isEmailVerified = info.EmailVerified;
            isAnonymous = false;
        }

        internal UserInfo(VerifyAssertionResponse info)
        {
            uid = info.LocalId;
            federatedId = info.FederatedId;
            firstName = info.FirstName;
            lastName = info.LastName;
            displayName = info.DisplayName;
            email = info.Email;
            isEmailVerified = info.EmailVerified;
            photoUrl = info.PhotoUrl;
            isAnonymous = false;
        }

        internal UserInfo(GetAccountInfoResponseUserInfo info)
        {
            uid = info.LocalId;
            federatedId = info.ProviderUserInfo?.FirstOrDefault(i => i.FederatedId != null)?.FederatedId;
            displayName = info.DisplayName;
            email = info.Email;
            isEmailVerified = info.EmailVerified;
            photoUrl = info.PhotoUrl;
            isAnonymous = false;
        }

        [SerializeField]
        private string uid;

        public string Uid => uid;

        [SerializeField]
        private string federatedId;

        public string FederatedId => federatedId;

        [SerializeField]
        private string firstName;

        public string FirstName => firstName;

        [SerializeField]
        private string lastName;

        public string LastName => lastName;

        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private string email;

        public string Email => email;

        [SerializeField]
        private bool isEmailVerified;

        public bool IsEmailVerified => isEmailVerified;

        [SerializeField]
        private string photoUrl;

        public string PhotoUrl => photoUrl;

        [SerializeField]
        private bool isAnonymous;

        public bool IsAnonymous => isAnonymous;
    }
}
