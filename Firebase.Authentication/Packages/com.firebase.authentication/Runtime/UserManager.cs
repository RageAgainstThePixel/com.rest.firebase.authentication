// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Repository;
using System;

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

        public FirebaseUser GetUser
        {
            get
            {
                if (cache != null)
                {
                    return cache;
                }

                if (!userRepository.UserExists)
                {
                    return null;
                }

                return cache = userRepository.GetUser;
            }
        }

        public void SaveNewUser(FirebaseUser user)
        {
            cache = user;
            userRepository.SaveUser(user);
            UserChanged?.Invoke(user);
        }

        public void UpdateExistingUser(FirebaseUser user)
        {
            if (user.Uid != cache?.Info.Uid)
            {
                SaveNewUser(user);
            }
        }

        public void DeleteExistingUser(string uid)
        {
            if (cache?.Info.Uid != uid)
            {
                // deleting a user which is not an active user
                return;
            }

            cache = null;

            userRepository.DeleteUser();

            UserChanged?.Invoke(null);
        }
    }
}
