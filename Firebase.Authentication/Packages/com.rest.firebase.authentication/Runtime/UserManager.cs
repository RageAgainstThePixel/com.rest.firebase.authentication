// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using Firebase.Rest.Authentication.CredentialStore;
using Utilities.Async;

namespace Firebase.Rest.Authentication
{
    internal class UserManager
    {
        private readonly IUserCredentialStore userCredentialStore;

        private FirebaseUser cache;

        public event Action<FirebaseUser> UserChanged;

        /// <summary>
        /// Creates a new instance of <see cref="UserManager"/> with custom repository.
        /// </summary>
        /// <param name="fileSystem"> Proxy to the filesystem operations. </param>
        public UserManager(IUserCredentialStore fileSystem)
        {
            userCredentialStore = fileSystem;
        }

        public FirebaseUser GetUser
        {
            get
            {
                if (cache != null)
                {
                    return cache;
                }

                if (!userCredentialStore.UserExists)
                {
                    return null;
                }

                return cache = userCredentialStore.GetUser;
            }
        }

        public async Task SaveNewUserAsync(FirebaseUser user)
        {
            cache = user;
            await userCredentialStore.SaveUserAsync(user);
            // Raise events only on main thread.
            await Awaiters.UnityMainThread;
            UserChanged?.Invoke(user);
        }

        public async Task UpdateExistingUserAsync(FirebaseUser user)
        {
            if (cache?.Info.Uid != user.Uid)
            {
                await SaveNewUserAsync(user);
            }
        }

        public async Task DeleteExistingUserAsync(string uid)
        {
            if (cache?.Info.Uid == uid)
            {
                cache = null;
                await userCredentialStore.DeleteUserAsync();
            }

            // Raise events only on main thread.
            await Awaiters.UnityMainThread;
            UserChanged?.Invoke(null);
        }
    }
}
