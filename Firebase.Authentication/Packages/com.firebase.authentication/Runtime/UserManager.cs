using System;
using System.Threading.Tasks;
using Firebase.Authentication.Repository;

namespace Firebase.Authentication
{
    internal class UserManager
    {
        private readonly IUserRepository userRepository;

        private FirebaseUser cache;

        public event Action<FirebaseUser> UserChanged;

        /// <summary>
        /// Creates a new instance of <see cref="UserManager"/> with custom repository.
        /// </summary>
        /// <param name="fileSystem"> Proxy to the filesystem operations. </param>
        public UserManager(IUserRepository fileSystem)
        {
            userRepository = fileSystem;
        }

        public async Task<FirebaseUser> GetUserAsync()
        {
            if (cache != null)
            {
                return cache;
            }

            if (!await userRepository.UserExistsAsync().ConfigureAwait(false))
            {
                return null;
            }

            return cache = await userRepository.ReadUserAsync().ConfigureAwait(false);
        }

        public async Task SaveNewUserAsync(FirebaseUser user)
        {
            cache = user;

            await userRepository.SaveUserAsync(user).ConfigureAwait(false);

            UserChanged?.Invoke(user);
        }

        public Task UpdateExistingUserAsync(FirebaseUser user)
        {
            return user.Uid != cache?.Info.Uid
                ? Task.CompletedTask
                : SaveNewUserAsync(user);
        }

        public async Task DeleteExistingUserAsync(string uid)
        {
            if (cache?.Info.Uid != uid)
            {
                // deleting a user which is not an active user
                return;
            }

            cache = null;

            await userRepository.DeleteUserAsync().ConfigureAwait(false);

            UserChanged?.Invoke(null);
        }
    }
}
