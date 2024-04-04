// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Firebase.Rest.Authentication.CredentialStore
{
    /// <summary>
    /// Repository abstraction for <see cref="FirebaseUser"/>.
    /// </summary>
    public interface IUserCredentialStore
    {
        bool UserExists { get; }

        FirebaseUser GetUser { get; }

        Task SaveUserAsync(FirebaseUser newUser);

        Task DeleteUserAsync();
    }
}
