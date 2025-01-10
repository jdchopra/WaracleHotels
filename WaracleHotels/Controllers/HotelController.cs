using Microsoft.AspNetCore.Mvc;
using WaracleHotels.Models;

namespace WaracleHotels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Hotel), 200)]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;

        public HotelController(ILogger<HotelController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHotel")]
        public async Task<ActionResult<Hotel>> Get(string name)
        {
            var hotel = new Hotel
            {
                Name = name
            };

            return Ok(await Task.FromResult(hotel));
        }
    }
}
