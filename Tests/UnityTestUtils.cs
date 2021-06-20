// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Firebase.Authentication.Tests
{
    /// <summary>
    /// Adopted from https://answers.unity.com/questions/1597151/async-unit-test-in-test-runner.html
    /// </summary>
    internal static class UnityTestUtils
    {
        public static T RunAsyncTestsAsSync<T>(Func<Task<T>> asyncFunc)
        {
            return Task.Run(async () => await asyncFunc()).Result;
        }

        public static void RunAsyncTestsAsSync(Func<Task> asyncFunc)
        {
            Task.Run(async () => await asyncFunc()).Wait();
        }
    }
}
