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

        private AuthResponse? _authInfo = new() {AccessToken = "This is just to show reauthorize functionality"};

        public AuthService(IOptions<HotelSearchApiSettings> options, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _searchApiSettings = options.Value;
        }

        public async Task<string> GetBearerToken()
        {
            if (IsTokenExpired())
            {
                await GetToken();
            }

            return "Bearer " + _authInfo?.AccessToken;
        }

        public async Task Reauthorize() => await GetToken();

        private bool IsTokenExpired() =>
            // TODO: Implement expiration logic
            false;

        private async Task GetToken()
        {
            var client = _httpClientFactory.CreateClient("auth-client");

            var getTokenResponse = await client.PostAsync("token", ApiKeyAndSecret());

            if (getTokenResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AuthenticationException("Could not get authenticated with the API key and Secret");
            }

            getTokenResponse.EnsureSuccessStatusCode();
            var json = await getTokenResponse.Content.ReadAsStringAsync();

            _authInfo = JsonConvert.DeserializeObject<AuthResponse>(json);
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