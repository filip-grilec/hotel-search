﻿using System.Threading.Tasks;

namespace HotelSearch.Authentication
{
    public interface IAuthService
    {
        Task Authenticate();
    }
}