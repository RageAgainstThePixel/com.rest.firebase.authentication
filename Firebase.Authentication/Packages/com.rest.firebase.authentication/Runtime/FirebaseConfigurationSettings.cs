// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace Firebase.Rest.Authentication
{
    [CreateAssetMenu(fileName = nameof(FirebaseConfigurationSettings), menuName = "Firebase/" + nameof(FirebaseConfigurationSettings))]
    internal class FirebaseConfigurationSettings : ScriptableObject
    {
        [SerializeField]
        private string projectId;

        public string ProjectId => projectId;

        [SerializeField]
        [Tooltip("The api key of your Firebase app.")]
        private string apiKey;

        /// <summary>
        /// The api key of your Firebase app.
        /// </summary>
        public string ApiKey => apiKey;

        [SerializeField]
        [Tooltip("project-id.firebaseapp.com")]
        private string authDomain;

        /// <summary>
        /// Auth domain of your firebase app (project-id.firebaseapp.com).
        /// </summary>
        public string AuthDomain => authDomain;
    }
}
