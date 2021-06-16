namespace Firebase.Authentication.Repository
{
    /// <summary>
    /// Repository abstraction for <see cref="FirebaseUser"/>.
    /// </summary>
    internal interface IUserRepository
    {
        bool UserExists { get; }

        FirebaseUser GetUser { get; }

        void SaveUser(FirebaseUser newUser);

        void DeleteUser();
    }
}
