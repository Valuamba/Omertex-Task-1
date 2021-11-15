using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string PassengerFirstName { get; set; }

        public string PassengerLastName { get; set; }

        public string PassengerDocumentNumber { get; set; }

        public int PassengerSeatNumber { get; set; }

        public TicketStatus Status { get; set; }

    }
}
