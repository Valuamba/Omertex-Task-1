using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using BusManager.Application.Services;
using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Services.Interfaces;

namespace BusManager.API.Controllers
{
    [ApiController]
    [Route("api/voyage")]
    public class VoyageController : ControllerBase
    {
        private readonly IVoyageService _voyageService;
        private readonly ILogger<VoyageController> _logger;

        public VoyageController(IVoyageService voyageService, ILogger<VoyageController> logger)
        {
            _voyageService = voyageService;
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(VoyageInfoRequest[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVoyages()
        {
            try
            {
                var voyages = await _voyageService.GetVoyages();

                return Ok(voyages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{voyageId:int}")]
        [ProducesResponseType(typeof(VoyageInfoRequest), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVoyage(int voyageId)
        {
            try
            {
                var voyage = await _voyageService.GetVoyage(voyageId);

                return Ok(voyage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/{from?}")]
        [ProducesResponseType(typeof(VoyageInfoRequest[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchVoyages(string from = null, [FromQuery] string to = null, [FromQuery] DateTime? departureDateTime = null, [FromQuery] string voyageName = null)
        {
            try
            {
                var voyage = await _voyageService.SearchVoyages(from, to, departureDateTime, voyageName);

                return Ok(voyage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
