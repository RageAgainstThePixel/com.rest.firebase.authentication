using System.Threading.Tasks;

namespace Firebase.Authentication.Repository
{
    /// <summary>
    /// <see cref="IUserRepository"/> implementation which saves user data in memory.
    /// </summary>
    /// <inheritdoc />
    public class InMemoryRepository : IUserRepository
    {
        private FirebaseUser user;

        public Task<bool> UserExistsAsync()
        {
            return Task.FromResult(user != null);
        }

        public Task<FirebaseUser> ReadUserAsync()
        {
            return Task.FromResult(user);
        }

        public Task SaveUserAsync(FirebaseUser newUser)
        {
            user = newUser;
            return Task.CompletedTask;
        }

        public Task DeleteUserAsync()
        {
            user = null;
            return Task.CompletedTask;
        }
    }
}
