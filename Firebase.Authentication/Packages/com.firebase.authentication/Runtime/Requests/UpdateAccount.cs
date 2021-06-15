namespace Firebase.Authentication.Requests
{
    internal class UpdateAccount : FirebaseRequestBase<UpdateAccountRequest, UpdateAccountResponse>
    {
        public UpdateAccount(FirebaseConfiguration config)
            : base(config)
        {
        }

        protected override string UrlFormat => Endpoints.GoogleUpdateUserPassword;
    }
}
