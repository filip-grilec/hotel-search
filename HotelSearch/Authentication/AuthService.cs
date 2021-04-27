using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HotelSearch.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HotelSearchApiSettings _searchApiSettings;

        private AuthResponse? _authInfo;
        private readonly int _defaultExpireInSeconds = 1800;
        private DateTime _tokenReceivedUtc;

        public AuthService(IOptions<HotelSearchApiSettings> options, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _searchApiSettings = options.Value;
        }

        public async Task<string> AuthHeader()
        {
            if (IsTokenExpired())
            {
                var token = await GetTokenAsync();
                SaveToken(token);
            }

            return "Bearer " + _authInfo?.AccessToken;
        }

        private async Task<AuthResponse> GetTokenAsync()
        {
            var client = _httpClientFactory.CreateClient("auth-client");

            var getTokenResponse = await client.PostAsync("token", ApiKeyAndSecret());

            if (getTokenResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AuthenticationException(
                    "Could not get authenticated with the API key and Secret, check credentials");
            }

            var json = await getTokenResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthResponse>(json);
        }

        private bool IsTokenExpired() => TokenValidTo() < DateTime.UtcNow;

        private DateTime TokenValidTo() =>
            _tokenReceivedUtc.AddSeconds(_authInfo?.ExpiresIn ?? _defaultExpireInSeconds);

        private void SaveToken(AuthResponse authResponse)
        {
            _authInfo = authResponse;
            _tokenReceivedUtc = DateTime.UtcNow;
        }

        private FormUrlEncodedContent ApiKeyAndSecret()
        {
            return new(new KeyValuePair<string?, string?>[]
            {
                new("client_id", _searchApiSettings.ClientId),
                new("grant_type", _searchApiSettings.GrantType),
                new("client_secret", _searchApiSettings.ClientSecret)
            });
        }
    }
}