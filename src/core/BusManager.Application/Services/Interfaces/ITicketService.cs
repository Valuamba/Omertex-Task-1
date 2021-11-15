using BusManager.Application.Contracts.Ticket;
using System.Threading.Tasks;

namespace BusManager.Application.Services.Interfaces
{
    public interface ITicketService
    {
        TicketRepsonse[] GetTickets(int userId);

        Task<TicketRepsonse> GetTicket(int ticketId);

        Task<BuyTicketResponse> BuyBackReservedTicket(int ticketId);
    }
}
