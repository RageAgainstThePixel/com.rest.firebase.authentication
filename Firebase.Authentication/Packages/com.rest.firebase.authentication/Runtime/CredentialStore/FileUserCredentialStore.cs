// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Firebase.Rest.Authentication.CredentialStore
{
    /// <summary>
    /// <see cref="IUserCredentialStore"/> implementation which saves user data in specified folder.
    /// </summary>
    /// <inheritdoc />
    public sealed class FileUserCredentialStore : AbstractUserCredentialStore
    {
        private readonly string filename;

        /// <summary>
        /// Creates new instance of <see cref="FileUserCredentialStore"/>.
        /// </summary>
        /// <param name="cacheDirectory"> Name of the subfolder to be created / accessed under <see cref="Application.persistentDataPath"/>.</param>
        public FileUserCredentialStore(string cacheDirectory)
        {
            var data = Application.persistentDataPath;
            filename = Path.Combine(data, cacheDirectory, "firebase.json");
            Directory.CreateDirectory(Path.Combine(data, cacheDirectory));
        }

        public override bool UserExists => File.Exists(filename);

        public override FirebaseUser GetUser
        {
            get
            {
                var content = File.ReadAllText(filename);
                var obj = JsonUtility.FromJson<StoredUser>(content);
                return new FirebaseUser(Configuration, obj.UserInfo, obj.Credential);
            }
            protected set => throw new NotImplementedException();
        }

        public override async Task SaveUserAsync(FirebaseUser newUser)
        {
            var content = JsonUtility.ToJson(new StoredUser(newUser.Info, newUser.Credential));
            await File.WriteAllTextAsync(filename, content);
        }

        public override async Task DeleteUserAsync()
        {
            File.Delete(filename);
            await Task.CompletedTask;
        }
    }
}
