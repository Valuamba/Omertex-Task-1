using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicket(Ticket newTicket);

        Task<Ticket> GetTicketByIdAsync(int ticketId);

        Task UpdateTicket(Ticket ticket);
    }
}
