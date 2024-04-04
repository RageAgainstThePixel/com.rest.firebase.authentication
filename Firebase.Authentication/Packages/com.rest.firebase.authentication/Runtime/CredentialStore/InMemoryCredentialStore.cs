// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Firebase.Rest.Authentication.CredentialStore
{
    /// <summary>
    /// <see cref="IUserCredentialStore"/> implementation which saves user data in memory.
    /// </summary>
    /// <inheritdoc />
    public sealed class InMemoryCredentialStore : AbstractUserCredentialStore
    {
        public override bool UserExists => GetUser != null;

        public override FirebaseUser GetUser { get; protected set; }

        public override async Task SaveUserAsync(FirebaseUser newUser)
        {
            GetUser = newUser;
            await Task.CompletedTask;
        }

        public override async Task DeleteUserAsync()
        {
            GetUser = null;
            await Task.CompletedTask;
        }
    }
}
