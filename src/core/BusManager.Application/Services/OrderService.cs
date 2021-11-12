using BusManager.Application.Contracts.Order;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using BusManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusManager.Application.Services.Interfaces;

namespace BusManager.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IVoyagesRepository _voyagesRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ITicketRepository ticketRepository,
            IVoyagesRepository voyagesRepository)
        {
            _orderRepository = orderRepository;
            _ticketRepository = ticketRepository;
            _voyagesRepository = voyagesRepository;
        }

        public async Task<ProcessOrderResponse> ProcessOrder(int userId, TicketOrder ticketOrder)
        {
            if(ticketOrder.PassengerSeatNumber == null)
            {
                throw new Exception("The passenger seat should be taken.");
            }

            var ticket = new Domain.Models.Ticket()
            {
                PassengerDocumentNumber = ticketOrder.PassengerDocumentNumber,
                PassengerFirstName = ticketOrder.PassengerFirstName,
                PassengerLastName = ticketOrder.PassengerLastName,
                PassengerSeatNumber = ticketOrder.PassengerSeatNumber.Value,
                Status = ticketOrder.TicketStatus,
            };

            int voyageId = ticketOrder.VoyageId;

            var orders = _orderRepository.GetOrdersByVoyageIdAsync(voyageId);

            bool isSeatAlreadyReserved = orders.Any(o => o.Ticket.PassengerSeatNumber == ticket.PassengerSeatNumber);

            if (isSeatAlreadyReserved)
            {
                throw new Exception("The passenger seat already reserved.");
            }

            if(orders.Any(o => o.Ticket.PassengerFirstName == ticket.PassengerFirstName && o.Ticket.PassengerLastName == ticket.PassengerLastName))
            {
                throw new Exception("The passenger already reserved ticket.");
            }

            if (string.IsNullOrWhiteSpace(ticket.PassengerFirstName) || string.IsNullOrWhiteSpace(ticket.PassengerLastName))
            {
                throw new Exception("Incorrect first name or last name.");
            }

            var voyage = await _voyagesRepository.GetVoyageByIdAsync(voyageId);

            if (voyage.DepartureDateTime.CompareTo(DateTime.Now) == -1)
            {
                throw new Exception("The voyage was outdated.");
            }

            var order = new Domain.Models.Order()
            {
                UserId = userId,
                Status = ticket.Status == TicketStatus.Reserved ? OrderStatus.Reserved : OrderStatus.BoughtOut,
                VoyageId = voyageId
            };

            var newOrder = await _orderRepository.AddOrder(order);

            ticket.OrderId = newOrder.OrderId;

            var newTicket = await _ticketRepository.AddTicket(ticket);

            return new() { TicketId = newTicket.TicketId, IsSuccessful = true };
        }
    }
}
