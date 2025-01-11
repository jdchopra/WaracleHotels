using Microsoft.AspNetCore.Mvc;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;
using WaracleHotels.Services;

namespace WaracleHotels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Booking), 200)]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet(Name = "GetBooking")]
        public async Task<ActionResult<Booking>> Get(int bookingReference)
        {
            var booking = await _bookingService.GetBookingAsync(bookingReference);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost(Name = "PostBooking")]
        public async Task<ActionResult<int>> Post([FromBody] BookingRequest bookingRequest)
        {
            var bookingReference = await _bookingService.AddBooking(bookingRequest);

            return Ok(bookingReference);
        }
    }
}
