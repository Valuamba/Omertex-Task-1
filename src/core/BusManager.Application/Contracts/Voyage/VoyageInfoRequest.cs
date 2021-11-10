using System;
using System.Collections.Generic;

namespace BusManager.Application.Contracts.Voyage
{
    public class VoyageInfoRequest
    {
        public int VoyageId { get; set; }
        public string DepartureBusStopName { get; set; }

        public string ArrivalBusStopName { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public int TravelTimeMinutes { get; set; }

        public string VoyageNumber { get; set; }

        public string VoyageName { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal OneTicketCost { get; set; }

        public bool IsPossibleToOrder { get; set; }

        public List<SeatInfo> SeatsInfo { get; set; }
    }

    public struct SeatInfo
    {
        public int SeatNumber { get; set; }
        public bool IsSeatTaken { get; set; }
    }
}
