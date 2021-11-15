using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using BusManager.API.Helpers;
using System.Threading.Tasks;
using BusManager.Application.Services.Interfaces;
using BusManager.Application.Contracts.Ticket;

namespace BusManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet("user/all")]
        [ProducesResponseType(typeof(Application.Contracts.Ticket.TicketRepsonse[]), (int)HttpStatusCode.OK)]
        public IActionResult GetUserTickets()
        {
            try
            {
                var tickets = _ticketService.GetTickets(ControllerHelper.GetUserId(this).Value);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
            
        [HttpGet("{ticketId:int}")]
        [ProducesResponseType(typeof(TicketRepsonse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            try
            {
                var ticket = await _ticketService.GetTicket(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buy/{ticketId:int}")]
        [ProducesResponseType(typeof(BuyTicketResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuyBackTicket(int ticketId)
        {
            try
            {
                var ticket = await _ticketService.BuyBackReservedTicket(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new BuyTicketResponse() { TicketId= ticketId, ErrorMessage = ex.Message} );
            }
        }
    }
}
