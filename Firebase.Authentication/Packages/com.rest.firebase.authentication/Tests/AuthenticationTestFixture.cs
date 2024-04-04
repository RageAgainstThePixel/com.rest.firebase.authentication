// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;
using System.Threading.Tasks;

namespace Firebase.Rest.Authentication.Tests
{
    internal class AuthenticationTestFixture
    {
        private const string email = "test@email.com";
        private const string password = "tempP@ssw0rd";

        [Test]
        public async Task Test_1_CreateUser()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);

            var user = await firebaseClient.CreateUserWithEmailAndPasswordAsync(email, password, "test user");

            Assert.NotNull(user);
            Assert.NotNull(firebaseClient.User);
            Assert.IsTrue(firebaseClient.IsUserLoggedIn);
            Assert.IsTrue(user.Info.Uid == firebaseClient.User.Uid);

            await firebaseClient.SignOutAsync();

            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }

        [Test]
        public async Task Test_2_UserFunctions()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);

            var user = await firebaseClient.SignInWithEmailAndPasswordAsync(email, password);
            var token = await user.GetIdTokenAsync();

            Assert.IsFalse(string.IsNullOrWhiteSpace(token));

            await user.ChangeDisplayNameAsync("new name");
            await firebaseClient.SignOutAsync();
            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }

        [Test]
        public async Task Test_3_DeleteUser()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);
            var user = await firebaseClient.SignInWithEmailAndPasswordAsync(email, password);

            Assert.NotNull(user);
            Assert.NotNull(firebaseClient.User);
            Assert.IsTrue(user.Info.Uid == firebaseClient.User.Uid);

            await user.DeleteAsync();

            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }
    }
}
