// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Firebase.Authentication.CredentialStore
{
    /// <summary>
    /// Repository abstraction for <see cref="FirebaseUser"/>.
    /// </summary>
    public abstract class AbstractUserCredentialStore : IUserCredentialStore
    {
        internal FirebaseConfiguration Configuration { get; set; }

        /// <inheritdoc />
        public abstract bool UserExists { get; }

        /// <inheritdoc />
        public abstract FirebaseUser GetUser { get; protected set; }

        /// <inheritdoc />
        public abstract Task SaveUserAsync(FirebaseUser newUser);

        /// <inheritdoc />
        public abstract Task DeleteUserAsync();
    }
}
