using System.Threading.Tasks;

namespace Firebase.Authentication.Repository
{
    /// <summary>
    /// Repository abstraction for <see cref="FirebaseUser"/>.
    /// </summary>
    internal interface IUserRepository
    {
        Task<bool> UserExistsAsync();

        Task<FirebaseUser> ReadUserAsync();

        Task SaveUserAsync(FirebaseUser newUser);

        Task DeleteUserAsync();
    }
}
