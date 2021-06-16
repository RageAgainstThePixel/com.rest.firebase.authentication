// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication
{
    /// <summary>
    /// Basic information about the signed in user.
    /// </summary>
    public class UserInfo
    {
        public string Uid { get; set; }

        public string FederatedId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
