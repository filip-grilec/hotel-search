using System;
using System.Threading.Tasks;

namespace HotelSearch.HotelSearch
{
    public interface IHotelSearchService
    {
        Task<(HotelResponse response, bool success)> GetHotels(FilterHotelsRequest request);
    }

    public class FilterHotelsRequest
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? Checkout { get; set; }
        public string? City { get; set; }
    }
}