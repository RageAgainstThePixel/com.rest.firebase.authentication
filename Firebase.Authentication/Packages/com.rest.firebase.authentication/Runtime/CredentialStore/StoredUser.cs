// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.CredentialStore
{
    [Serializable]
    internal class StoredUser
    {
        public StoredUser(UserInfo userInfo, FirebaseCredential credential)
        {
            this.userInfo = userInfo;
            this.credential = credential;
        }

        [SerializeField]
        private UserInfo userInfo;

        public UserInfo UserInfo => userInfo;

        [SerializeField]
        private FirebaseCredential credential;

        public FirebaseCredential Credential => credential;
    }
}
