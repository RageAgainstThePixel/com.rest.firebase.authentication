// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Runtime.Serialization;

namespace Firebase.Authentication
{
    /// <summary>
    /// Type of authentication provider.
    /// </summary>
    public enum FirebaseProviderType
    {
        Anonymous = 0,

        [EnumMember(Value = "facebook.com")]
        Facebook,

        [EnumMember(Value = "google.com")]
        Google,

        [EnumMember(Value = "github.com")]
        Github,

        [EnumMember(Value = "twitter.com")]
        Twitter,

        [EnumMember(Value = "microsoft.com")]
        Microsoft,

        [EnumMember(Value = "apple.com")]
        Apple,

        [EnumMember(Value = "password")]
        EmailAndPassword,

        [EnumMember(Value = "custom")]
        CustomToken,
    }
}
