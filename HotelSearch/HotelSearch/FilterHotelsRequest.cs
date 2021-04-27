using System;

namespace HotelSearch.HotelSearch
{
    public class FilterHotelsRequest
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? Checkout { get; set; }
        public string? City { get; set; }
    }
}