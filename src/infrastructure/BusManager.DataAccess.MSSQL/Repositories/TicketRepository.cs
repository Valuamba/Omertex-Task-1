using AutoMapper;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MssqlBusManagerDbContext _context;
        private readonly IMapper _mapper;

        public TicketRepository(MssqlBusManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Ticket> AddTicket(Ticket newTicket)
        {
            var ticket = _mapper.Map<Ticket, Entities.Ticket>(newTicket);

            var trackedTicket = await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return _mapper.Map<Entities.Ticket, Ticket>(trackedTicket.Entity);
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            var ticketEntity = _mapper.Map<Ticket, Entities.Ticket>(ticket);

            var trackedTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticket.TicketId);

            if (trackedTicket == null)
            {
                throw new ArgumentNullException(nameof(trackedTicket));
            }

            trackedTicket.PassengerDocumentNumber = ticketEntity.PassengerDocumentNumber;
            trackedTicket.PassengerFirstName = ticketEntity.PassengerFirstName;
            trackedTicket.PassengerLastName = ticketEntity.PassengerLastName;
            trackedTicket.PassengerSeatNumber = ticketEntity.PassengerSeatNumber;
            trackedTicket.Status = ticketEntity.Status;

            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await _context.Tickets.Include(t => t.Order).FirstOrDefaultAsync(t => t.TicketId == ticketId);

            return _mapper.Map<Entities.Ticket, Ticket>(ticket);
        }
    }
}
