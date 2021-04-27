using System.Threading.Tasks;

namespace HotelSearch.Authentication
{
    public interface IAuthService
    {
        Task<string> GetBearerToken();
        Task Reauthorize();
    }
}