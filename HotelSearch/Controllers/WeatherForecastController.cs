using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelSearch.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using QuickType;


namespace HotelSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMemoryCache _cache;


        private readonly AsyncRetryPolicy<HttpResponseMessage> httpWithReauthorize = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .RetryAsync(3, onRetryAsync: ReAuthorize);

        private static async Task ReAuthorize(DelegateResult<HttpResponseMessage> response, int retryCount)
        {
            // Reauthorize
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthService _authService;
        private readonly HotelSearchApiSettings _searchApiSettings;

        public WeatherForecastController(IMemoryCache cache, IHttpClientFactory httpClientFactory,
            IOptions<HotelSearchApiSettings> options, IAuthService authService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _authService = authService;
            _searchApiSettings = options.Value;
        }

        [HttpGet]
        public async Task<HotelsResponse> Get()
        {
            var client = _httpClientFactory.CreateClient("auth-client");


            var res = await client.PostAsync("token", new FormUrlEncodedContent(new KeyValuePair<string?, string?>[]
            {
                new("client_id", _searchApiSettings.ClientId),
                new("grant_type", _searchApiSettings.GrantType),
                new("client_secret", _searchApiSettings.ClientSecret)
            }));


            var json = await res.Content.ReadAsStringAsync();

            var authInfo = JsonConvert.DeserializeObject<AuthResponse>(json);


            var hotelClient = _httpClientFactory.CreateClient("hotel-search");


             var (authorization, bearerToken) = await _authService.GetAuthHeaders();
            hotelClient.DefaultRequestHeaders.Add(authorization, bearerToken);
            
            var hotelResponse = await hotelClient.GetAsync("hotel-offers?cityCode=PAR&checkInDate=2021-06-01&checkOutDate=2021-06-10&roomQuantity=1&adults=1&radius=100&radiusUnit=KM&paymentPolicy=NONE&includeClosed=true&bestRateOnly=true&view=FULL&sort=NONE&page%5Blimit%5D=50");



            return JsonConvert.DeserializeObject<HotelsResponse>(await hotelResponse.Content.ReadAsStringAsync());

        }
    }
}