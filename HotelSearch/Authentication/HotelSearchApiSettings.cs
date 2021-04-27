namespace HotelSearch.Authentication
{
    public class HotelSearchApiSettings
    {
        public readonly string GrantType = "client_credentials";
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}