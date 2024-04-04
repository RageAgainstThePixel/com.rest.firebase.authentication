// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Firebase.Rest.Authentication
{
    /// <summary>
    /// Delegate which should invoke the passed uri for oauth authentication and return the final redirect uri.
    /// </summary>
    /// <param name="uri"> Uri to take user to. </param>
    /// <returns> Redirect uri that user lands on. </returns>
    public delegate Task<string> SignInRedirectDelegate(string uri);
}
