using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Threading.Tasks;
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
        private readonly FirebaseConfiguration config;
        private readonly JsonSerializerSettings options;

        /// <summary>
        /// Creates new instance of <see cref="FileUserRepository"/>.
        /// </summary>
        /// <param name="config"><see cref="FirebaseConfiguration"/></param>
        /// <param name="folder"> Name of the subfolder to be created / accessed under <see cref="Application.persistentDataPath"/>. </param>
        public FileUserRepository(FirebaseConfiguration config, string folder)
        {
            this.config = config;
            var data = Application.persistentDataPath;
            filename = Path.Combine(data, folder, "firebase.json");
            options = new JsonSerializerSettings();
            options.Converters.Add(new StringEnumConverter());

            Directory.CreateDirectory(Path.Combine(data, folder));
        }

        public virtual Task<FirebaseUser> ReadUserAsync()
        {
            var content = File.ReadAllText(filename);
            var obj = JsonConvert.DeserializeObject<StoredUser>(content, options);
            return Task.FromResult(new FirebaseUser(config, obj.UserInfo, obj.Credential));
        }

        public virtual Task SaveUserAsync(FirebaseUser newUser)
        {
            var content = JsonConvert.SerializeObject(new StoredUser(newUser.Info, newUser.Credential), options);
            File.WriteAllText(filename, content);
            return Task.CompletedTask;
        }

        public virtual Task DeleteUserAsync()
        {
            File.Delete(filename);
            return Task.CompletedTask;
        }

        public Task<bool> UserExistsAsync()
        {
            return Task.FromResult(File.Exists(filename));
        }

        private class StoredUser
        {
            public StoredUser(UserInfo userInfo, FirebaseCredential credential)
            {
                UserInfo = userInfo;
                Credential = credential;
            }

            public UserInfo UserInfo { get; }

            public FirebaseCredential Credential { get; }
        }
    }
}
