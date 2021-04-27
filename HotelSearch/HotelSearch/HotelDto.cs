using System.Collections.Generic;
using Newtonsoft.Json;

namespace HotelSearch.HotelSearch
{
    public class HotelDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonProperty("availability")]
        public List<AvailabilityDto>? Availability { get; set; }
    }
}