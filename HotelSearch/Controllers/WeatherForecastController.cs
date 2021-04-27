using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HotelSearch.Authentication;
using HotelSearch.HotelSearch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace HotelSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IAuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(IAuthService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClientFactory = httpClientFactory;
        }

      
        [HttpGet]
        public async Task<HotelsResponse> Get()
        {
          
            var hotelClient = _httpClientFactory.CreateClient("hotel-search");

            hotelClient.DefaultRequestHeaders.Add("Authorization", await _authService.GetBearerToken());

            var hotelResponse = await hotelClient.GetAsync(
                "hotel-offers?cityCode=PAR&checkInDate=2021-06-01&checkOutDate=2021-06-10&roomQuantity=1&adults=1&radius=100&radiusUnit=KM&paymentPolicy=NONE&includeClosed=true&bestRateOnly=true&view=FULL&sort=NONE&page%5Blimit%5D=50");


            return JsonConvert.DeserializeObject<HotelsResponse>(await hotelResponse.Content.ReadAsStringAsync());
        }
    }
}