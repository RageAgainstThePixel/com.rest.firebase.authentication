// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Rest.Authentication.Requests
{
    [Serializable]
    internal class ResetPasswordResponse
    {
        [SerializeField]
        private string email;

        public string Email => email;
    }
}
