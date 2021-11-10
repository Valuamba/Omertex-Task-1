using BusManager.Domain.Models;

namespace BusManager.Application.Contracts.Order
{
    public class TicketOrder
    {
        public int VoyageId { get; set; }

        public string PassengerFirstName { get; set; }

        public string PassengerLastName { get; set; }

        public string PassengerDocumentNumber { get; set; }

        public int PassengerSeatNumber { get; set; }

        public TicketStatus TicketStatus { get; set; }
    }
}
