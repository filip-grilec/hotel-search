using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelSearch.Authentication;
using Newtonsoft.Json;
using Polly.Retry;

namespace HotelSearch.HotelSearch
{
    public class HotelSearchService : IHotelSearchService
    {
        private readonly IAuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;

        private const int Persons = 1;
        private const int SearchRadius = 100;
        private const bool IncludeClosedHotels = true;

        public HotelSearchService(IAuthService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(HotelResponse? response, bool success)> GetHotels(FilterHotelsRequest request)
        {
            var hotels = await SearchHotels(request);

            if (hotels?.Data?.Count == 0)
            {
                return (null, false);
            }

            return (MapResponse(hotels), true);
        }

        private static HotelResponse? MapResponse(AmadeusHotelsResponse? hotels)
        {
            var hotelDtos = hotels?.Data?.Select(h => new HotelDto()
            {
                Rating = h.Hotel.Rating,
                Description = h.Hotel.Description.Text,
                Name = h.Hotel.Name,
                IsAvailable = h.Available,
                Availability = h.Offers.Select(o => new AvailabilityDto()
                {
                    Price = o.Price.Total,
                    CheckIn = o.CheckInDate,
                    CheckOut = o.CheckOutDate
                }).ToList()
            }).ToList();

            return new HotelResponse() {Hotels = hotelDtos};
        }

        private async Task<AmadeusHotelsResponse?> SearchHotels(FilterHotelsRequest request)
        {
            var client = _httpClientFactory.CreateClient("hotel-search");

            client.DefaultRequestHeaders.Add("Authorization", await _authService.GetBearerToken());

            var filteredHotelsResponse = await client.GetAsync("hotel-offers?" + BuildQuery(request));

            var hotels =
                JsonConvert.DeserializeObject<AmadeusHotelsResponse>(await filteredHotelsResponse.Content
                    .ReadAsStringAsync());
            return hotels;
        }

        private string BuildQuery(FilterHotelsRequest filters) =>
            $"cityCode={filters.City}" +
            $"&checkInDate={FormatDate(filters.CheckIn)}&checkOutDate={FormatDate(filters.Checkout)}" +
            $"&roomQuantity=1&adults={Persons}" +
            $"&radius={SearchRadius}&radiusUnit=KM" +
            $"&paymentPolicy=NONE" +
            $"&includeClosed={IncludeClosedHotels}" +
            $"&bestRateOnly=true&view=FULL&sort=NONE";

        private string FormatDate(DateTime? dateTime) => dateTime.HasValue ?  dateTime.Value.ToString("s").Split("T")[0] : string.Empty;
    }
}