using System.Threading.Tasks;

namespace HotelSearch.Authentication
{
    public interface IAuthService
    {
        Task Authenticate();
        Task<(string, string)> GetAuthHeaders();

    }
}