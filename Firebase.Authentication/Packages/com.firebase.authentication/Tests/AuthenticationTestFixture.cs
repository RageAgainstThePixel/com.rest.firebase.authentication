// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;

namespace Firebase.Authentication.Tests
{
    internal class AuthenticationTestFixture
    {
        [Test]
        public void Test_1_CreateUser()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);

            var user = firebaseClient.CreateUserWithEmailAndPasswordAsync("test@email.com", "tempP@ssw0rd", "test user").Result;

            Assert.NotNull(user);
            Assert.NotNull(firebaseClient.User);
            Assert.IsTrue(firebaseClient.IsUserLoggedIn);
            Assert.IsTrue(user.Info.Uid == firebaseClient.User.Uid);

            firebaseClient.SignOut();

            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }

        [Test]
        public void Test_2_UserFunctions()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);

            var user = firebaseClient.SignInWithEmailAndPasswordAsync("test@email.com", "tempP@ssw0rd").Result;
            var token = user.GetIdTokenAsync().Result;

            Assert.IsFalse(string.IsNullOrWhiteSpace(token));

            user.ChangeDisplayNameAsync("new name").Wait();

            firebaseClient.SignOut();
            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }

        [Test]
        public void Test_3_DeleteUser()
        {
            var firebaseClient = new FirebaseAuthenticationClient();
            Assert.NotNull(firebaseClient);
            var user = firebaseClient.SignInWithEmailAndPasswordAsync("test@email.com", "tempP@ssw0rd").Result;

            Assert.NotNull(user);
            Assert.NotNull(firebaseClient.User);
            Assert.IsTrue(user.Info.Uid == firebaseClient.User.Uid);

            user.DeleteAsync().Wait();

            Assert.IsFalse(firebaseClient.IsUserLoggedIn);
        }
    }
}
