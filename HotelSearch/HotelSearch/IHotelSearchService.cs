using System.Threading.Tasks;

namespace HotelSearch.HotelSearch
{
    public interface IHotelSearchService
    {
        Task<(HotelResponse? response, bool success)> GetHotels(FilterHotelsRequest request);
    }
}