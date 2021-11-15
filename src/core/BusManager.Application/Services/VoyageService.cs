using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Paging;
using BusManager.Application.Services.Interfaces;
using BusManager.Domain.Models;
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

        public async Task<VoyageInfoRequest> GetVoyage(int voyageId)
        {
            var voyage = await _voyagesRepository.GetVoyageByIdAsync(voyageId);

            GetInfoAboutVoyageSeats(voyage, out List<SeatInfo> SeatsInfo, out bool isPossibleToOrder);

            return new VoyageInfoRequest()
            {
                VoyageId = voyage.Id,
                ArrivalBusStopName = voyage.ArrivalBusStop.Name,
                ArrivalDateTime = voyage.ArrivalDateTime,
                DepartureBusStopName = voyage.DepartureBusStop.Name,
                DepartureDateTime = voyage.DepartureDateTime,
                NumberOfSeats = voyage.NumberOfSeats,
                OneTicketCost = voyage.OneTicketCost,
                TravelTimeMinutes = voyage.TravelTimeMinutes,
                VoyageName = voyage.VoyageName,
                VoyageNumber = voyage.VoyageNumber,
                IsPossibleToOrder = isPossibleToOrder,
                SeatsInfo = SeatsInfo
            };
        }

        public async Task<PagedList<VoyageInfoRequest>> GetVoyages(VoyageParameters voyageParameters)
        {
            var voyages = await _voyagesRepository.GetVoyages(
                voyageParameters.PageNumber, voyageParameters.PageSize, 
                voyageParameters.From, voyageParameters.To,
                voyageParameters.DepartureDate, voyageParameters.VoyageName);

            return PagedList<VoyageInfoRequest>.ToPagedList(voyages.AsEnumerable().Select(v =>
            {
                GetInfoAboutVoyageSeats(v, out List<SeatInfo> SeatsInfo, out bool isPossibleToOrder);

                return new VoyageInfoRequest()
                {
                    VoyageId = v.Id, 
                    DepartureBusStopName = v.DepartureBusStop.Name,
                    ArrivalBusStopName = v.ArrivalBusStop.Name,
                    ArrivalDateTime = v.ArrivalDateTime,
                    DepartureDateTime = v.DepartureDateTime,
                    NumberOfSeats = v.NumberOfSeats,
                    OneTicketCost = v.OneTicketCost,
                    TravelTimeMinutes = v.TravelTimeMinutes,
                    VoyageName = v.VoyageName,
                    VoyageNumber = v.VoyageNumber,
                    IsPossibleToOrder = isPossibleToOrder
                };
            }), voyageParameters.PageNumber, voyageParameters.PageSize);
        }

        private void GetInfoAboutVoyageSeats(VoyageInfo voyage, out List<SeatInfo> seatsInfo, out bool isPossibleToOrder)
        {
            seatsInfo = new();
            var takedSeatNumbers = voyage.Orders.Select(x => x.Ticket.PassengerSeatNumber);
            isPossibleToOrder = voyage.ArrivalDateTime.CompareTo(DateTime.Now) > 0;

            for (int seat = 1; seat <= voyage.NumberOfSeats; seat++)
            {
                var isSeatTaken = takedSeatNumbers?.Any(takedSeat => takedSeat == seat) ?? false;

                if (!isSeatTaken)
                {
                    isPossibleToOrder = true;
                }

                seatsInfo.Add(new SeatInfo()
                {
                    SeatNumber = seat,
                    IsSeatTaken = isSeatTaken
                });
            }
        }
    }
}
