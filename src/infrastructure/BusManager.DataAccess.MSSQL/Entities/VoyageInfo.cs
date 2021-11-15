using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Entities
{
    public class VoyageInfo
    {
        public int Id { get; set; }

        public int DepartureBusStopId { get; set; }
        public BusStop DepartureBusStop { get; set; }

        public int ArrivalBusStopId { get; set; }
        public BusStop ArrivalBusStop { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public int TravelTimeMinutes { get; set; }

        public string VoyageNumber { get; set; }

        public string VoyageName { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal OneTicketCost { get; set; }

        public List<Order> Orders { get; set; }
    }
}
