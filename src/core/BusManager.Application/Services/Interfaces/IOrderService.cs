using BusManager.Application.Contracts.Order;
using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ProcessOrderResponse> ProcessOrder(int userId, TicketOrder ticketOrder);
    }
}
