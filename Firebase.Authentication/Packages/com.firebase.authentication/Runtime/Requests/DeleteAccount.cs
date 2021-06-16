// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Firebase.Authentication.Requests
{
    /// <summary>
    /// Deletes user's account.
    /// </summary>
    internal class DeleteAccount : FirebaseRequestBase<IdTokenRequest, object>
    {
        public DeleteAccount(FirebaseConfiguration configuration)
            : base(configuration)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleDeleteUserUrl;
    }
}
