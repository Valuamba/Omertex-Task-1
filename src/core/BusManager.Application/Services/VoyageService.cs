using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Services.Interfaces;
using BusManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services
{
    public class VoyageService : IVoyageService
    {
        private readonly IVoyagesRepository _voyagesRepository;

        public VoyageService(IVoyagesRepository voyagesRepository)
        {
            _voyagesRepository = voyagesRepository;
        }

        public async Task<VoyageInfoRequest[]> SearchVoyages(string from = null, string to = null, DateTime? departureTime = null, string voyageName = null)
        {
            var voyages = await _voyagesRepository.SearchVoyage(from, to, departureTime, voyageName);

            return voyages.AsEnumerable().Select(v => new VoyageInfoRequest()
            {
                DepartureBusStopName = v.DepartureBusStop.Name,
                ArrivalBusStopName = v.ArrivalBusStop.Name,
                ArrivalDateTime = v.ArrivalDateTime,
                DepartureDateTime = v.DepartureDateTime,
                NumberOfSeats = v.NumberOfSeats,
                OneTicketCost = v.OneTicketCost,
                TravelTimeMinutes = v.TravelTimeMinutes,
                VoyageName = v.VoyageName,
                VoyageNumber = v.VoyageNumber
            }).ToArray();
        }

        public async Task<VoyageInfoRequest> GetVoyage(int voyageId)
        {
            var voyage = await _voyagesRepository.GetVoyageByIdAsync(voyageId);

            return new VoyageInfoRequest()
            {
                ArrivalBusStopName = voyage.ArrivalBusStop.Name,
                ArrivalDateTime = voyage.ArrivalDateTime,
                DepartureBusStopName = voyage.DepartureBusStop.Name,
                DepartureDateTime = voyage.DepartureDateTime,
                NumberOfSeats = voyage.NumberOfSeats,
                OneTicketCost = voyage.OneTicketCost,
                TravelTimeMinutes = voyage.TravelTimeMinutes,
                VoyageName = voyage.VoyageName,
                VoyageNumber = voyage.VoyageNumber
            };
        }

        public async Task<VoyageInfoRequest[]> GetVoyages()
        {
            var voyages = await _voyagesRepository.GetVoyages();

            List<SeatInfo> SeatsInfo = new();
            List<VoyageInfoRequest> voyagesRequest = new(voyages.Count());

            foreach (var voyage in voyages)
            {
                var takedSeatNumbers = voyage.Orders.Select(x => x.Ticket.PassengerSeatNumber);
                bool isPossibleToOrder = false;

                for (int seat = 1; seat <= voyage.NumberOfSeats; seat++)
                {
                    var isSeatTaken = takedSeatNumbers?.Any(takedSeat => takedSeat == seat) ?? false;

                    if (!isSeatTaken)
                    {
                        isPossibleToOrder = true;
                    }

                    SeatsInfo.Add(new SeatInfo()
                    {
                        SeatNumber = seat,
                        IsSeatTaken = isSeatTaken
                    });
                }

                isPossibleToOrder = voyage.ArrivalDateTime.CompareTo(DateTime.Now) > 0;

                voyagesRequest.Add(new VoyageInfoRequest()
                {
                    ArrivalBusStopName = voyage.ArrivalBusStop.Name,
                    ArrivalDateTime = voyage.ArrivalDateTime,
                    DepartureBusStopName = voyage.DepartureBusStop.Name,
                    DepartureDateTime = voyage.DepartureDateTime,
                    NumberOfSeats = voyage.NumberOfSeats,
                    OneTicketCost = voyage.OneTicketCost,
                    TravelTimeMinutes = voyage.TravelTimeMinutes,
                    VoyageName = voyage.VoyageName,
                    VoyageNumber = voyage.VoyageNumber,
                    VoyageId = voyage.Id,
                    SeatsInfo = SeatsInfo,
                    IsPossibleToOrder = isPossibleToOrder
                });
            }

            return voyagesRequest.ToArray();
        }
    }
}
