namespace HotelSearch.Authentication
{
    public class HotelSearchApiSettings
    {
        public string? ClientId { get; set; }
        public readonly string GrantType = "client_credentials";
        public string? ClientSecret { get; set; }
    }
}