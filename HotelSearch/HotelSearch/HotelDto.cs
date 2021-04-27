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
        public decimal Description { get; set; }
    }
}