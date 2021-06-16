namespace Firebase.Authentication.Requests
{
    internal class UpdateAccount : FirebaseRequestBase<UpdateAccountRequest, UpdateAccountResponse>
    {
        public UpdateAccount(FirebaseConfiguration configuraton)
            : base(configuraton)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleUpdateUserPassword;
    }
}
