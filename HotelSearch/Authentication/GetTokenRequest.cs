﻿namespace HotelSearch.Authentication
{
    public class GetTokenRequest
    {
        public string grant_type { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
    }
}