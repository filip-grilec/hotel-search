using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HotelSearch.Authentication
{
    public class AuthServiceService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HotelSearchApiSettings _searchApiSettings;

        private string _token = string.Empty;
        private AuthResponse? _authInfo;

        public AuthServiceService(IOptions<HotelSearchApiSettings> options, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _searchApiSettings = options.Value;
        }
        public async Task Authenticate()
        {
            var client = _httpClientFactory.CreateClient("auth-client");
            
            var res = await client.PostAsync("token", new FormUrlEncodedContent(new KeyValuePair<string?, string?>[]
            {
                new("client_id", _searchApiSettings.ClientId),
                new("grant_type", _searchApiSettings.GrantType),
                new("client_secret", _searchApiSettings.ClientSecret)
            }));


            var json = await res.Content.ReadAsStringAsync();

            _authInfo = JsonConvert.DeserializeObject<AuthResponse>(json);
        }
    }
}