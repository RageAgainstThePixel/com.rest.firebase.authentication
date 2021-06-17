// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Object = UnityEngine.Object;

namespace Firebase.Authentication
{
    public class FirebaseAuthentication
    {
        /// <summary>
        /// Creates a new <see cref="FirebaseAuthentication"/> instance.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="apiKey">The api key.</param>
        /// <param name="authDomain">Optional, override auth domain to use. (Defaults to 'project-id.firebaseapp.com')</param>
        public FirebaseAuthentication(string projectId, string apiKey, string authDomain = null)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentException($"no {nameof(projectId)} provided.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"no {nameof(apiKey)} provided.");
            }

            ProjectId = projectId;
            ApiKey = apiKey;
            AuthDomain = authDomain ?? $"{projectId}.firebaseapp.com";
        }

        private static FirebaseAuthentication cachedDefault = null;

        public static FirebaseAuthentication Default
        {
            get
            {
                if (cachedDefault != null)
                {
                    return cachedDefault;
                }

                var auth = (LoadFromAsset() ??
                            LoadFromEnv() ??
                            LoadFromDirectory()) ??
                            LoadFromDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                cachedDefault = auth;
                return auth;
            }
            set => cachedDefault = value;
        }

        public string ProjectId { get; }

        public string ApiKey { get; }

        public string AuthDomain { get; }

        private static FirebaseAuthentication LoadFromAsset()
            => (from asset in Object.FindObjectsOfType<FirebaseConfigurationSettings>()
                where !string.IsNullOrWhiteSpace(asset.ApiKey) && !string.IsNullOrWhiteSpace(asset.AuthDomain) && !string.IsNullOrWhiteSpace(asset.ProjectId)
                select new FirebaseAuthentication(asset.ProjectId, asset.ApiKey, asset.AuthDomain)).FirstOrDefault();

        private static FirebaseAuthentication LoadFromEnv()
        {
            var path = Environment.GetEnvironmentVariable("FIREBASE_CONFIGURATION");

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.GetEnvironmentVariable("GOOGLE_FIREBASE_CONFIGURATION");
            }

            return LoadFromDirectory(path);
        }

        private static FirebaseAuthentication LoadFromDirectory(string directory = null, string filename = "google-services.json", bool searchUp = true)
        {
            directory = directory ?? Environment.CurrentDirectory;

            string projectId = null;
            string key = null;
            var currentDirectory = new DirectoryInfo(directory);

            while (key == null && currentDirectory.Parent != null)
            {
                if (File.Exists(Path.Combine(currentDirectory.FullName, filename)))
                {
                    var path = Path.Combine(currentDirectory.FullName, filename);
                    var json = File.ReadAllText(path);
                    var googleServices = JsonConvert.DeserializeObject<GoogleServices>(json);
                    projectId = googleServices.ProjectInfo.ProjectId;
                    key = googleServices.Client.FirstOrDefault()?.ApiKey.FirstOrDefault()?.CurrentKey;
                }

                if (searchUp)
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    break;
                }
            }

            return !string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(projectId)
                ? new FirebaseAuthentication(projectId, key)
                : null;
        }

        #region Google Services Data Object

        private class GoogleServices
        {
            [JsonProperty("project_info")]
            public ProjectInfo ProjectInfo { get; set; }

            [JsonProperty("client")]
            public List<Client> Client { get; set; }

            [JsonProperty("configuration_version")]
            public string ConfigurationVersion { get; set; }
        }

        private class ProjectInfo
        {
            [JsonProperty("project_number")]
            public string ProjectNumber { get; set; }

            [JsonProperty("firebase_url")]
            public string FirebaseUrl { get; set; }

            [JsonProperty("project_id")]
            public string ProjectId { get; set; }

            [JsonProperty("storage_bucket")]
            public string StorageBucket { get; set; }
        }

        private class AndroidClientInfo
        {
            [JsonProperty("package_name")]
            public string PackageName { get; set; }
        }

        private class ClientInfo
        {
            [JsonProperty("mobilesdk_app_id")]
            public string MobilesdkAppId { get; set; }

            [JsonProperty("android_client_info")]
            public AndroidClientInfo AndroidClientInfo { get; set; }
        }

        private class OauthClient
        {
            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            [JsonProperty("client_type")]
            public int ClientType { get; set; }
        }

        private class ApiKeyProperty
        {
            [JsonProperty("current_key")]
            public string CurrentKey { get; set; }
        }

        private class IosInfo
        {
            [JsonProperty("bundle_id")]
            public string BundleId { get; set; }
        }

        private class OtherPlatformOauthClient
        {
            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            [JsonProperty("client_type")]
            public int ClientType { get; set; }

            [JsonProperty("ios_info")]
            public IosInfo IosInfo { get; set; }
        }

        private class AppinviteService
        {
            [JsonProperty("other_platform_oauth_client")]
            public List<OtherPlatformOauthClient> OtherPlatformOauthClient { get; set; }
        }

        private class Services
        {
            [JsonProperty("appinvite_service")]
            public AppinviteService AppinviteService { get; set; }
        }

        private class Client
        {
            [JsonProperty("client_info")]
            public ClientInfo ClientInfo { get; set; }

            [JsonProperty("oauth_client")]
            public List<OauthClient> OauthClient { get; set; }

            [JsonProperty("api_key")]
            public List<ApiKeyProperty> ApiKey { get; set; }

            [JsonProperty("services")]
            public Services Services { get; set; }
        }

        #endregion Google Services Data Object
    }
}
