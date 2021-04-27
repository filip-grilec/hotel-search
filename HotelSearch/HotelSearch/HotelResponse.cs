using System.Collections.Generic;

namespace HotelSearch.HotelSearch
{
    public class HotelResponse
    {
        public List<HotelDto> Hotels { get; set; } = new();
    }
}