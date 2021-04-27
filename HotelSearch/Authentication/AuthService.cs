using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HotelSearch.Authentication
{
    public class AuthServiceService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HotelSearchApiSettings _searchApiSettings;

        private AuthResponse? _authInfo;

        public AuthServiceService(IOptions<HotelSearchApiSettings> options, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _searchApiSettings = options.Value;
        }
        public async Task Authenticate()
        {
            await GetToken();
        }

        public async Task<(string, string)> GetAuthHeaders()
        {
            return ("Authorization", "Bearer "+ _authInfo?.AccessToken);
        }

        private async Task GetToken()
        {
            var client = _httpClientFactory.CreateClient("auth-client");

            var getTokenResponse = await client.PostAsync("token", ApiKeyAndSecret());

            if (getTokenResponse.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthenticationException("Could not get authenticated with the API key and Secret");

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