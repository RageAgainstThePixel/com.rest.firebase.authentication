using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Authentication.Requests;

namespace Firebase.Authentication.Providers
{
    public abstract class OAuthProvider : FirebaseAuthProvider
    {
        private VerifyAssertion verifyAssertion;

        private readonly List<string> scopes;
        private readonly Dictionary<string, string> parameters;

        protected abstract string[] DefaultScopes { get; }

        protected OAuthProvider(List<string> defaultScopes = null)
        {
            scopes = defaultScopes ?? new List<string>();
            parameters = new Dictionary<string, string>();
        }

        protected virtual string LocaleParameterName => null;

        protected static AuthCredential GetCredential(FirebaseProviderType providerType, string accessToken, OAuthCredentialTokenType tokenType)
        {
            return new OAuthCredential(accessToken, tokenType, providerType);
        }

        internal override void Initialize(FirebaseConfiguration configuration)
        {
            base.Initialize(configuration);
            verifyAssertion = new VerifyAssertion(configuration);
        }

        internal virtual AuthCredential GetCredential(VerifyAssertionResponse response)
        {
            return GetCredential(
                ProviderType,
                response.PendingToken ?? response.OauthAccessToken,
                response.PendingToken == null ? OAuthCredentialTokenType.AccessToken : OAuthCredentialTokenType.PendingToken);
        }

        internal virtual async Task<OAuthContinuation> SignInAsync()
        {
            if (LocaleParameterName != null && !parameters.ContainsKey(LocaleParameterName))
            {
                parameters[LocaleParameterName] = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }

            var response = await SendAuthRequest(new CreateAuthUriRequest
            {
                ContinueUri = Configuration.RedirectUri,
                ProviderId = ProviderType,
                CustomParameters = parameters,
                OauthScope = scopes.Any() ? $"{{ \"{ProviderType.ToEnumMemberString()}\": \"{string.Join(",", scopes)}\" }}" : null
            }).ConfigureAwait(false);

            return new OAuthContinuation(Configuration, response.AuthUri, response.SessionId, ProviderType);
        }

        protected internal override async Task<FirebaseUser> SignInWithCredentialAsync(AuthCredential credential)
        {
            var authCredential = (OAuthCredential)credential;
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(credential.ProviderType, new VerifyAssertionRequest
            {
                RequestUri = $"https://{Configuration.AuthDomain}",
                PostBody = authCredential.GetPostBodyValue(credential.ProviderType),
                PendingToken = authCredential.GetPendingTokenValue(),
                ReturnIdpCredential = true,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            credential = GetCredential(response);

            response.Validate(credential);
            return user;
        }

        protected internal override async Task<FirebaseUser> LinkWithCredentialAsync(string idToken, AuthCredential credential)
        {
            var authCredential = (OAuthCredential)credential;
            var (user, response) = await verifyAssertion.ExecuteAndParseAsync(credential.ProviderType, new VerifyAssertionRequest
            {
                IdToken = idToken,
                RequestUri = $"https://{Configuration.AuthDomain}",
                PostBody = authCredential.GetPostBodyValue(authCredential.ProviderType),
                PendingToken = authCredential.GetPendingTokenValue(),
                ReturnIdpCredential = true,
                ReturnSecureToken = true
            }).ConfigureAwait(false);

            credential = GetCredential(response);

            response.Validate(credential);

            return user;
        }

        protected class OAuthCredential : AuthCredential
        {
            public OAuthCredential(string accessToken, OAuthCredentialTokenType tokenType, FirebaseProviderType providerType)
                : base(providerType)
            {
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
