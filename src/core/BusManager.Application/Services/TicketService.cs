using AutoMapper;
using BusManager.Application.Contracts.Ticket;
using BusManager.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using BusManager.Application.Services.Interfaces;

namespace BusManager.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IVoyagesRepository _voyagesRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IVoyagesRepository voyagesRepository, IOrderRepository orderRepository)
        {
            _ticketRepository = ticketRepository;
            _voyagesRepository = voyagesRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public TicketRepsonse[] GetTickets(int userId)
        {
            var tickets = _orderRepository.GetOrdersByUserId(userId).Where(o => o.Ticket != null).Select(o => o.Ticket);

            return tickets.Select(t =>
                new TicketRepsonse()
                {
                    Id = t.TicketId,
                    NumberOfSeats = t.Order.Voyage.NumberOfSeats,
                    PassengerFirstName = t.PassengerFirstName,
                    PassengerLastName = t.PassengerLastName,
                    OneTicketCost = t.Order.Voyage.OneTicketCost,
                    ArrivalDateTime = t.Order.Voyage.ArrivalDateTime,
                    DepartureDateTime = t.Order.Voyage.DepartureDateTime,
                    Arrival = t.Order.Voyage.ArrivalBusStop.Name,
                    Departure = t.Order.Voyage.DepartureBusStop.Name,
                    PassengerDocumentNumber = t.PassengerDocumentNumber,
                    PassengerSeatNumber = t.PassengerSeatNumber,
                    Status = t.Status,
                    TravelTimeMinutes = t.Order.Voyage.TravelTimeMinutes,
                    VoyageName = t.Order.Voyage.VoyageName,
                    VoyageNumber = t.Order.Voyage.VoyageNumber
                }).ToArray();
        }

        public async Task<TicketRepsonse> GetTicket(int ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

            return _mapper.Map<Domain.Models.Ticket, TicketRepsonse>(ticket);
        }

        public async Task<BuyTicketResponse> BuyBackReservedTicket(int ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            var voyage = await _voyagesRepository.GetVoyageByIdAsync(ticket.Order.VoyageId);

            if (ticket.Status != Domain.Models.TicketStatus.Reserved)
            {
                throw new Exception("The ticket should have status Reserved.");
            }

            if (ticket.Order.Status != Domain.Models.OrderStatus.Reserved)
            {
                throw new Exception("The order should have status Reserved.");
            }

            if (voyage.DepartureDateTime.CompareTo(DateTime.Now) == -1)
            {
                throw new Exception("The ticket was outdated.");
            }

            ticket.Status = Domain.Models.TicketStatus.BoughtOut;
            ticket.Order.Status = Domain.Models.OrderStatus.BoughtOut;

            await _ticketRepository.UpdateTicket(ticket);
            await _orderRepository.UpdateOrder(ticket.Order);

            return new BuyTicketResponse()
            {
                TicketId = ticketId,
                IsSuccessful = true
            };
        }
    }
}
