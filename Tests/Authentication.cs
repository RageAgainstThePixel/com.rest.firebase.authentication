// Copyright (c) Stephen Hodgson. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;

namespace Firebase.Authentication.Tests
{
    internal class Authentication
    {
        [Test]
        public void Test_1()
        {
            var firebaseClient = new FirebaseAuthenticationClient("apiKey", "hello.firebase.com");
            Assert.NotNull(firebaseClient);
        }
    }
}
