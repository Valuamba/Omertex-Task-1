using BusManager.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BusManager.Application.Contracts.Order
{
    public class TicketOrder
    {
        public int VoyageId { get; set; }

        [Required(ErrorMessage = "Passenger first name is required.")]
        public string PassengerFirstName { get; set; }

        [Required(ErrorMessage = "Passenger last name is required.")]
        public string PassengerLastName { get; set; }

        [Required(ErrorMessage = "Passenger document number is required.")]
        public string PassengerDocumentNumber { get; set; }

        [Required(ErrorMessage = "Passenger seat is required.")]
        public int? PassengerSeatNumber { get; set; }

        public TicketStatus TicketStatus { get; set; }
    }
}
