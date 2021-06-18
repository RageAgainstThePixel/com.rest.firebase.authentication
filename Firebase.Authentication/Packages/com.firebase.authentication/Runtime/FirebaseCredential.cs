// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Firebase.Authentication
{
    /// <summary>
    /// Firebase credentials used to make Firebase requests.
    /// </summary>
    [Serializable]
    public class FirebaseCredential
    {
        internal FirebaseCredential(string idToken, string refreshToken, int expiresIn, FirebaseProviderType providerType)
        {
            created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.idToken = idToken;
            this.refreshToken = refreshToken;
            this.expiresIn = expiresIn;
            this.providerType = providerType.ToString();
        }

        [SerializeField]
        private string idToken;

        /// <summary>
        /// Value of the token to be used with Firebase requests.
        /// </summary>
        public string IdToken => idToken;

        [SerializeField]
        private string refreshToken;

        /// <summary>
        /// Value of the refresh token which can be used to refresh the <see cref="IdToken"/>.
        /// </summary>
        public string RefreshToken => refreshToken;

        [SerializeField]
        private long created;

        /// <summary>
        /// Specifies when the token was created.
        /// </summary>
        public DateTime Created => DateTimeOffset.FromUnixTimeSeconds(created).DateTime;

        [SerializeField]
        private int expiresIn;

        /// <summary>
        /// Specifies in how many second the token expires in from the moment it was created.
        /// </summary>
        public int ExpiresIn => expiresIn;

        [SerializeField]
        private string providerType;

        /// <summary>
        /// Type of the firebase auth provider.
        /// </summary>
        public FirebaseProviderType ProviderType
        {
            get
            {
                if (Enum.TryParse<FirebaseProviderType>(providerType, true, out var value))
                {
                    return value;
                }

                throw new Exception($"Failed to parse {value} to {nameof(FirebaseProviderType)}");
            }
        }

        /// <summary>
        /// Specifies whether the token already expired.
        /// </summary>
        public bool IsExpired()
            // include a small 10s window when the token is technically valid
            // but it's a good idea to refresh it already.
            => DateTime.UtcNow > Created.AddSeconds(ExpiresIn - 10);
    }
}
