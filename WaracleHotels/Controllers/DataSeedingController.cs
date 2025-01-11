using Microsoft.AspNetCore.Mvc;
using WaracleHotels.Models;
using WaracleHotels.Services;

namespace WaracleHotels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Booking), 200)]
    public class DataSeedingController : ControllerBase
    {
        private readonly IDataSeedingService _dataSeedingService;

        public DataSeedingController(IDataSeedingService dataSeedingService)
        {
            _dataSeedingService = dataSeedingService;
        }

        [HttpDelete(Name = "DeleteData")]
        public async Task<ActionResult> Delete()
        {
            await _dataSeedingService.TearDown();
            return Ok();
        }

        [HttpGet(Name = "CreateData")]
        public async Task<ActionResult> Get()
        {
            await _dataSeedingService.Initialize();
            return Created();
        }
    }
}
