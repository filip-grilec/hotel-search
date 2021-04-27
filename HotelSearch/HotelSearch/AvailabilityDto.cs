using System;

namespace HotelSearch.HotelSearch
{
    public class AvailabilityDto
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal Price { get; set; }
    }
}