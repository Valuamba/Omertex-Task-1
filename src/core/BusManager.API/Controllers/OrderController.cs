using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Net;
using BusManager.Application.Contracts.Order;
using BusManager.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using BusManager.Application.Services.Interfaces;

namespace BusManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProcessOrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Process(TicketOrder ticketOrder)
        {
            try
            {
                var userId = ControllerHelper.GetUserId(this);
                var processResponse = await _orderService.ProcessOrder(userId.Value, ticketOrder);

                return Ok(processResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ProcessOrderResponse() { ErrorMessage = ex.Message });
            }
        }
    }
}
