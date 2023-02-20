// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using UnityEngine;

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
            AuthDomain = authDomain;

            if (string.IsNullOrWhiteSpace(AuthDomain))
            {
                AuthDomain = $"{projectId}.firebaseapp.com";
            }
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

                var authentication = (LoadFromAsset() ??
                                      LoadFromDirectory()) ??
                                      LoadFromDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) ??
                                      LoadFromEnv();

                cachedDefault = authentication ?? throw new AuthenticationException("Failed to load the default authentication credentials!");
                return authentication;
            }
            set => cachedDefault = value;
        }

        public string ProjectId { get; }

        public string ApiKey { get; }

        public string AuthDomain { get; }

        private static FirebaseAuthentication LoadFromAsset()
            => (Resources.LoadAll<FirebaseConfigurationSettings>(string.Empty)
                .Where(asset => asset != null)
                .Where(asset => !string.IsNullOrWhiteSpace(asset.ProjectId) && !string.IsNullOrWhiteSpace(asset.ApiKey))
                .Select(asset => new FirebaseAuthentication(asset.ProjectId, asset.ApiKey, asset.AuthDomain))).FirstOrDefault();

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
                    var googleServices = JsonUtility.FromJson<GoogleServices>(json);
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

            return !string.IsNullOrWhiteSpace(projectId) && !string.IsNullOrWhiteSpace(key)
                ? new FirebaseAuthentication(projectId, key)
                : null;
        }

        #region Google Services Data Object

        [Serializable]
        private class GoogleServices
        {
            [SerializeField]
            private ProjectInfo project_info;

            public ProjectInfo ProjectInfo => project_info;

            [SerializeField]
            private List<Client> client;

            public List<Client> Client => client;

            [SerializeField]
            private string configuration_version;

            public string ConfigurationVersion => configuration_version;
        }

        [Serializable]
        private class ProjectInfo
        {
            [SerializeField]
            private string project_number;

            public string ProjectNumber => project_number;

            [SerializeField]
            private string firebase_url;

            public string FirebaseUrl => firebase_url;

            [SerializeField]
            private string project_id;

            public string ProjectId => project_id;

            [SerializeField]
            private string storage_bucket;

            public string StorageBucket => storage_bucket;
        }

        [Serializable]
        private class AndroidClientInfo
        {
            [SerializeField]
            private string package_name;

            public string PackageName => package_name;
        }

        [Serializable]
        private class ClientInfo
        {
            [SerializeField]
            private string mobilesdk_app_id;

            public string MobileSdkAppId => mobilesdk_app_id;

            [SerializeField]
            private AndroidClientInfo android_client_info;

            public AndroidClientInfo AndroidClientInfo => android_client_info;
        }

        [Serializable]
        private class OauthClient
        {
            [SerializeField]
            private string client_id;

            public string ClientId => client_id;

            [SerializeField]
            private int client_type;

            public int ClientType => client_type;
        }

        [Serializable]
        private class ApiKeyProperty
        {
            [SerializeField]
            private string current_key;

            public string CurrentKey => current_key;
        }

        [Serializable]
        private class IosInfo
        {
            [SerializeField]
            private string bundle_id;

            public string BundleId => bundle_id;
        }

        [Serializable]
        private class OtherPlatformOauthClient
        {
            [SerializeField]
            private string client_id;

            public string ClientId => client_id;

            [SerializeField]
            private int client_type;

            public int ClientType => client_type;

            [SerializeField]
            private IosInfo ios_info;

            public IosInfo IosInfo => ios_info;
        }

        [Serializable]
        private class AppInviteService
        {
            [SerializeField]
            private List<OtherPlatformOauthClient> other_platform_oauth_client;

            public List<OtherPlatformOauthClient> OtherPlatformOauthClient => other_platform_oauth_client;
        }

        [Serializable]
        private class Services
        {
            [SerializeField]
            private AppInviteService appinvite_service;

            public AppInviteService AppInviteService => appinvite_service;
        }

        [Serializable]
        private class Client
        {
            [SerializeField]
            private ClientInfo client_info;

            public ClientInfo ClientInfo => client_info;

            [SerializeField]
            private List<OauthClient> oauth_client;

            public List<OauthClient> OauthClient => oauth_client;

            [SerializeField]
            private List<ApiKeyProperty> api_key;

            public List<ApiKeyProperty> ApiKey => api_key;

            [SerializeField]
            private Services services;

            public Services Services => services;
        }

        #endregion Google Services Data Object
    }
}
