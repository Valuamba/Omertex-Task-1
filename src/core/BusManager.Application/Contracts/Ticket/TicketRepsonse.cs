using BusManager.Domain.Models;
using System;

namespace BusManager.Application.Contracts.Ticket
{
    public class TicketRepsonse
    {
        public string PassengerFirstName { get; set; }

        public string PassengerLastName { get; set; }

        public string PassengerDocumentNumber { get; set; }

        public int PassengerSeatNumber { get; set; }

        public TicketStatus Status { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public int TravelTimeMinutes { get; set; }

        public string VoyageNumber { get; set; }

        public string VoyageName { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal OneTicketCost { get; set; }
    }
}
