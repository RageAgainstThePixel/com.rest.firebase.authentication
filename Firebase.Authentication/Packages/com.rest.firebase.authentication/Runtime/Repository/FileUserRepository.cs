// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using UnityEngine;

namespace Firebase.Authentication.Repository
{
    /// <summary>
    /// <see cref="IUserRepository"/> implementation which saves user data application data folder.
    /// </summary>
    /// <inheritdoc />
    internal class FileUserRepository : IUserRepository
    {
        private readonly string filename;
        private readonly FirebaseConfiguration configuration;

        /// <summary>
        /// Creates new instance of <see cref="FileUserRepository"/>.
        /// </summary>
        /// <param name="configuration"><see cref="FirebaseConfiguration"/></param>
        /// <param name="cacheDirectory"> Name of the subfolder to be created / accessed under <see cref="Application.persistentDataPath"/>.</param>
        public FileUserRepository(FirebaseConfiguration configuration, string cacheDirectory)
        {
            this.configuration = configuration;
            var data = Application.persistentDataPath;
            filename = Path.Combine(data, cacheDirectory, "firebase.json");

            Directory.CreateDirectory(Path.Combine(data, cacheDirectory));
        }

        public FirebaseUser GetUser
        {
            get
            {
                var content = File.ReadAllText(filename);
                var obj = JsonUtility.FromJson<StoredUser>(content);
                return new FirebaseUser(configuration, obj.UserInfo, obj.Credential);
            }
        }

        public void SaveUser(FirebaseUser newUser)
        {
            var content = JsonUtility.ToJson(new StoredUser(newUser.Info, newUser.Credential));
            File.WriteAllText(filename, content);
        }

        public void DeleteUser()
        {
            File.Delete(filename);
        }

        public bool UserExists => File.Exists(filename);

        [Serializable]
        private class StoredUser
        {
            public StoredUser(UserInfo userInfo, FirebaseCredential credential)
            {
                this.userInfo = userInfo;
                this.credential = credential;
            }

            [SerializeField]
            private UserInfo userInfo;

            public UserInfo UserInfo => userInfo;

            [SerializeField]
            private FirebaseCredential credential;

            public FirebaseCredential Credential => credential;
        }
    }
}
