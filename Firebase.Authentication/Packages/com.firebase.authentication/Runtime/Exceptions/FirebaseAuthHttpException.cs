// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Firebase.Authentication.Exceptions
{
    /// <summary>
    /// Exception thrown during invocation of an HTTP request.
    /// </summary>
    public class FirebaseAuthHttpException : FirebaseAuthException
    {
        internal FirebaseAuthHttpException(Exception innerException, string requestUrl, string requestData, string responseData, AuthErrorReason reason = AuthErrorReason.Undefined)
            : base(GenerateExceptionMessage(requestUrl, requestData, responseData, reason), innerException, reason)
        {
            RequestUrl = requestUrl;
            RequestData = requestData;
            ResponseData = responseData;
        }

        /// <summary>
        /// Json data passed to the authentication service.
        /// </summary>
        public string RequestData { get; }

        /// <summary>
        /// Url for which the request failed.
        /// </summary>
        public string RequestUrl { get; }

        /// <summary>
        /// Response from the authentication service.
        /// </summary>
        public string ResponseData { get; }

        private static string GenerateExceptionMessage(string requestUrl, string requestData, string responseData, AuthErrorReason errorReason)
        {
            return $"Exception occurred during Firebase Http request!\nUrl: {requestUrl}\nRequest Data: {requestData}\nResponse: {responseData}\nReason: {errorReason}";
        }
    }
}
