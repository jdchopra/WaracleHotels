using Microsoft.AspNetCore.Mvc;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;
using WaracleHotels.Services;

namespace WaracleRooms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Room), 200)]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;    
        }

        [HttpPost]
        [Route("~/Available")]
        public async Task<ActionResult<Room[]>> QueryAvailableRooms([FromBody]AvailableRoomsRequest availableRoomsRequest)
        {
            var rooms = await _roomService.FindAvailableRooms(availableRoomsRequest);

            return Ok(rooms);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRoomRequest createRoomRequest)
        {
            await _roomService.AddRoom(createRoomRequest);

            return Created();
        }
    }
}
