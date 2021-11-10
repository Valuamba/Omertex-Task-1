using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using BusManager.Application.Services.BusStop;
using BusManager.Application.Contracts.BusStop;
using BusManager.Application.Services.Interfaces;

namespace BusManager.API.Controllers
{
    [ApiController]
    [Route("api/bus/stop")]
    public class BusStopController : ControllerBase
    {
        private readonly IBusStopService _busStopService;

        public BusStopController(IBusStopService busStopService)
        {
            _busStopService = busStopService;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(BusStopRequest[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBusStops()
        {
            var busStops = await _busStopService.GetBusStops();

            return Ok(busStops);
        }
    }
}
