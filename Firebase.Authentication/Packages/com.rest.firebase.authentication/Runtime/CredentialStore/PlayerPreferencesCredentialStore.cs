// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.Async;
using Utilities.Async.Internal;

namespace Firebase.Authentication.CredentialStore
{
    /// <summary>
    /// <see cref="IUserCredentialStore"/> implementation which saves user data in <see cref="PlayerPrefs"/>.
    /// </summary>
    /// <inheritdoc />
    public sealed class PlayerPreferencesCredentialStore : AbstractUserCredentialStore
    {
        private static string prefsKey;
        private static string PrefsKey => prefsKey ??= $"{nameof(FirebaseCredential)}_{Application.productName}";

        /// <inheritdoc />
        public override bool UserExists => PlayerPrefs.HasKey(PrefsKey);

        /// <inheritdoc />
        public override FirebaseUser GetUser
        {
            get
            {
                if (!SyncContextUtility.IsMainThread)
                {
                    throw new AccessViolationException($"{nameof(GetUser)} can only be called from the main thread!");
                }

                var storedUser = JsonUtility.FromJson<StoredUser>(Encoding.UTF8.GetString(Convert.FromBase64String(PlayerPrefs.GetString(PrefsKey))));
                return new FirebaseUser(Configuration, storedUser.UserInfo, storedUser.Credential);
            }
            protected set => throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override async Task SaveUserAsync(FirebaseUser newUser)
        {
            await Awaiters.UnityMainThread;
            PlayerPrefs.SetString(PrefsKey, Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtility.ToJson(new StoredUser(newUser.Info, newUser.Credential)))));
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public override async Task DeleteUserAsync()
        {
            await Awaiters.UnityMainThread;
            PlayerPrefs.DeleteKey(PrefsKey);
            await Task.CompletedTask;
        }
    }
}
