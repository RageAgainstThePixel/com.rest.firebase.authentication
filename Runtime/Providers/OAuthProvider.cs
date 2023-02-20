// Licensed under the MIT License. See LICENSE in the project root for license information.

using Firebase.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Firebase.Authentication.Providers
{
    public abstract class OAuthProvider : FirebaseAuthProvider
    {
        private VerifyAssertion verifyAssertion;

        private readonly List<string> scopes;
        private readonly Dictionary<string, string> parameters;

        protected abstract List<string> defaultScopes { get; }

        public IReadOnlyList<string> DefaultScopes => defaultScopes;

        /// <summary>
        /// Creates a new instance of the <see cref="OAuthProvider"/>
        /// </summary>
        /// <param name="scopes">Additional scopes to grant other than the <see cref="DefaultScopes"/> provided.</param>
        public OAuthProvider(List<string> scopes = null)
        {
            this.scopes = scopes ?? new List<string>();
            parameters = new Dictionary<string, string>();
        }

        protected virtual string LocaleParameterName => null;

        protected static AuthCredential GetCredential(FirebaseProviderType providerType, string accessToken, OAuthCredentialTokenType tokenType)
            => new OAuthCredential(accessToken, tokenType, providerType);

        protected internal override async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            var authCredential = (OAuthCredential)credential;
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(credential.ProviderType,
                new VerifyAssertionRequest(
                    idToken: null,
                    $"https://{Configuration.AuthDomain}",
                    authCredential.GetPostBodyValue(credential.ProviderType),
                    authCredential.GetPendingTokenValue()))
                .ConfigureAwait(false);
            credential = GetCredential(response);
            response.Validate(credential);
            return user;
        }

        protected internal override async Task<FirebaseUser> LinkWithCredentialAsync(string idToken, AuthCredential credential)
        {
            var authCredential = (OAuthCredential)credential;
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(credential.ProviderType,
                new VerifyAssertionRequest(
                    idToken,
                    $"https://{Configuration.AuthDomain}",
                    authCredential.GetPostBodyValue(authCredential.ProviderType),
                    authCredential.GetPendingTokenValue()))
                .ConfigureAwait(false);
            credential = GetCredential(response);
            response.Validate(credential);
            return user;
        }

        internal override void Initialize(FirebaseConfiguration configuration)
        {
            base.Initialize(configuration);
            verifyAssertion = new VerifyAssertion(configuration);

            foreach (var scope in defaultScopes)
            {
                if (!scopes.Contains(scope))
                {
                    scopes.Add(scope);
                }
            }
        }

        internal virtual AuthCredential GetCredential(VerifyAssertionResponse response)
        {
            return GetCredential(
                ProviderType,
                response.PendingToken ?? response.OauthAccessToken,
                response.PendingToken == null
                    ? OAuthCredentialTokenType.AccessToken
                    : OAuthCredentialTokenType.PendingToken);
        }

        internal async Task<OAuthContinuation> SignInAsync()
        {
            if (LocaleParameterName != null &&
                !parameters.ContainsKey(LocaleParameterName))
            {
                parameters[LocaleParameterName] = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }

            var response = await SendAuthRequest(new CreateOAuthUriRequest(ProviderType, Configuration.RedirectUri, parameters, scopes.Any() ? $"{{ \"{ProviderType.ToEnumMemberString()}\": \"{string.Join(",", scopes)}\" }}" : null, null)).ConfigureAwait(false);

            return new OAuthContinuation(Configuration, response.AuthUri, response.SessionId, ProviderType);
        }

        protected class OAuthCredential : AuthCredential
        {
            public OAuthCredential(string accessToken, OAuthCredentialTokenType tokenType, FirebaseProviderType providerType)
                : base(providerType)
            {
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    throw new InvalidOperationException($"{nameof(accessToken)} is invalid");
                }

                Token = accessToken;
                TokenType = tokenType;
            }

            private string Token { get; }

            private OAuthCredentialTokenType TokenType { get; }

            internal virtual string GetPostBodyValue(FirebaseProviderType providerType)
            {
                string tokenType;

                switch (TokenType)
                {
                    case OAuthCredentialTokenType.IdToken:
                        tokenType = "id_token";
                        break;
                    case OAuthCredentialTokenType.PendingToken:
                        tokenType = null;
                        break;
                    case OAuthCredentialTokenType.AccessToken:
                        tokenType = "access_token";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(providerType));
                }

                return tokenType == null
                    ? null
                    : $"{tokenType}={Token}&providerId={providerType.ToEnumMemberString()}";
            }

            internal string GetPendingTokenValue()
            {
                return TokenType == OAuthCredentialTokenType.PendingToken
                    ? Token
                    : null;
            }
        }
    }
}
