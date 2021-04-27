using System.Net.Http;
using System.Threading.Tasks;
using HotelSearch.Authentication;

namespace HotelSearch.HotelSearch
{
    public class HotelSearchService : IHotelSearchService
    {
        private readonly IAuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelSearchService(IAuthService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClientFactory = httpClientFactory;
        }
        public Task<(HotelResponse response, bool success)> GetHotels(FilterHotelsRequest request)
        {
            var client
            return null;
        }
    }
}