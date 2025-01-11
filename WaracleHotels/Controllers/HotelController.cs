using Microsoft.AspNetCore.Mvc;
using WaracleHotels.Models;
using WaracleHotels.Services;

namespace WaracleHotels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Hotel), 200)]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;    
        }

        [HttpGet(Name = "GetHotel")]
        public async Task<ActionResult<Hotel>> Get(string name)
        {
            var hotel = await _hotelService.GetHotelAsync(name);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost(Name = "PostHotel")]
        public async Task<ActionResult> Post(string hotelName)
        {
            await _hotelService.AddHotel(hotelName);

            return Created();
        }
    }
}
