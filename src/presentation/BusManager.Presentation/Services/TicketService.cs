using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusManager.Application.Contracts.Ticket;

namespace BusManager.Presentation.Services
{
    public interface ITicketService
    {
        Task<TicketRepsonse[]> GetAllTickets();

        Task<BuyTicketResponse> BuyBackTicket(int ticketId);
    }

    public class TicketService : ITicketService
    {
        private IHttpService _httpService;

        public TicketService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<TicketRepsonse[]> GetAllTickets()
        {
            return await _httpService.Get<TicketRepsonse[]>("api/ticket/user/all");
        }

        public async Task<BuyTicketResponse> BuyBackTicket(int ticketId)
        {
            return await _httpService.Get<BuyTicketResponse>($"api/ticket/buy/{ticketId}");
        }
    }
}
