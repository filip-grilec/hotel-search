using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelSearch.Authentication;
using Newtonsoft.Json;

namespace HotelSearch.HotelSearch
{
    public class HotelSearchService : IHotelSearchService
    {
        private const int Persons = 1;
        private const int SearchRadius = 100;
        private const bool IncludeClosedHotels = true;
        private readonly IAuthService _authService;
        private readonly IHttpClientFactory _httpClientFactory;


        public HotelSearchService(IAuthService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(HotelResponse? response, bool success)> GetHotels(FilterHotelsRequest request)
        {
            var hotels = await SearchHotels(request);

            return (MapResponse(hotels), HasData(hotels));
        }

        private async Task<AmadeusHotelsResponse?> SearchHotels(FilterHotelsRequest request)
        {
            var client = _httpClientFactory.CreateClient("hotel-search");

            client.DefaultRequestHeaders.Add("Authorization", await _authService.AuthHeader());

            var filteredHotelsResponse = await client.GetAsync("hotel-offers?" + BuildQuery(request));

            var hotels =
                JsonConvert.DeserializeObject<AmadeusHotelsResponse>(await filteredHotelsResponse.Content
                    .ReadAsStringAsync());
            return hotels;
        }

        private static bool HasData(AmadeusHotelsResponse? hotels) => hotels?.Data?.Count > 0;

        private static HotelResponse? MapResponse(AmadeusHotelsResponse? hotels)
        {
            var hotelDtos = new List<HotelDto>();

            foreach (var hotel in hotels?.Data ?? new List<HotelOffers>())
            {
                var hotelDto = new HotelDto
                {
                    Rating = hotel.Hotel.Rating,
                    Description = hotel.Hotel.Description?.Text,
                    Name = hotel.Hotel.Name,
                    IsAvailable = hotel.Available
                };

                if (hotel?.Offers != null)
                {
                    hotelDto.Availability.AddRange(hotel.Offers.Select(o => new AvailabilityDto
                    {
                        Price = o.Price.Total,
                        CheckIn = o.CheckInDate,
                        CheckOut = o.CheckOutDate
                    }));
                }

                hotelDtos.Add(hotelDto);
            }


            return new HotelResponse {Hotels = hotelDtos};
        }

        private string BuildQuery(FilterHotelsRequest filters) =>
            $"cityCode={filters.City}" +
            $"&checkInDate={FormatDate(filters.CheckIn)}&checkOutDate={FormatDate(filters.Checkout)}" +
            $"&roomQuantity=1&adults={Persons}" +
            $"&radius={SearchRadius}&radiusUnit=KM" +
            "&paymentPolicy=NONE" +
            $"&includeClosed={IncludeClosedHotels}" +
            "&bestRateOnly=true&view=FULL&sort=NONE";

        private string FormatDate(DateTime? dateTime) =>
            dateTime.HasValue ? dateTime.Value.ToString("s").Split("T")[0] : string.Empty;
    }
}