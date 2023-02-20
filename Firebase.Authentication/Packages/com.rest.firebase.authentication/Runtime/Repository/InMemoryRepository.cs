// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Repository
{
    /// <summary>
    /// <see cref="IUserRepository"/> implementation which saves user data in memory.
    /// </summary>
    /// <inheritdoc />
    public class InMemoryRepository : IUserRepository
    {
        public bool UserExists => GetUser != null;

        public FirebaseUser GetUser { get; private set; }

        public void SaveUser(FirebaseUser newUser)
        {
            GetUser = newUser;
        }

        public void DeleteUser()
        {
            GetUser = null;
        }
    }
}
