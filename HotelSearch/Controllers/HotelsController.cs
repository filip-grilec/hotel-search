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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelSearchService _hotelSearchService;

        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IAuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelsController(IHotelSearchService hotelSearchService)
        {
            _hotelSearchService = hotelSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchHotels([FromQuery] FilterHotelsRequest request)
        {
            var (response, success) = await _hotelSearchService.GetHotels(request);
            return success ? Ok(response) : NoContent();
        }
    }
}