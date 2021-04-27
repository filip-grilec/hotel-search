using System.Threading.Tasks;
using HotelSearch.HotelSearch;
using Microsoft.AspNetCore.Mvc;

namespace HotelSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelSearchService _hotelSearchService;

        public HotelsController(IHotelSearchService hotelSearchService)
        {
            _hotelSearchService = hotelSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchHotels([FromQuery] FilterHotelsRequest request)
        {
            var (response, success) = await _hotelSearchService.GetHotels(request);
            return success ? Ok(response) : NoContent();
        }
    }
}