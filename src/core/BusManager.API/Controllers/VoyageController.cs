using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using BusManager.Application.Services;
using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BusManager.Application.Paging;

namespace BusManager.API.Controllers
{
    [ApiController]
    [Route("api/voyage")]
    [Authorize]
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
        [ProducesResponseType(typeof(PagedList<VoyageInfoRequest>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVoyages([FromQuery] VoyageParameters voyageParameters)
        {
            try
            {
                var voyages = await _voyageService.GetVoyages(voyageParameters);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(voyages.MetaData));
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
    }
}
