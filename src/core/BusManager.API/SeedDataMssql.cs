using BusManager.DataAccess.MSSQL;
using BusManager.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusManager.API
{
    public class SeedDataMssql
    {
        public static void AddData(MssqlBusManagerDbContext busManagerContext)
        {
            AddBusStops(busManagerContext);
            AddVoyages(busManagerContext);
        }

        public static void AddBusStops(MssqlBusManagerDbContext busManagerContext)
        {
            if (!busManagerContext.BusStops.Any())
            {
                List<BusStop> busStops = new()
                {
                    new BusStop() { Description = "В Минске", Name = "Минск-Центральный", Status = "открытая" },
                    new BusStop() { Name = "Минск-Восточный" },
                    new BusStop() { Description = "В Гродно", Name = "Гродненская" },
                    new BusStop() { Description = "В Бресте", Name = "Брест-Центральный" },
                    new BusStop() { Description = "В Витебске", Name = "Витебск" },
                };

                busManagerContext.BusStops.AddRange(busStops);
                busManagerContext.SaveChanges();
            }
        }

        public static void AddVoyages(MssqlBusManagerDbContext busManagerContext)
        {
            if (!busManagerContext.Voyages.Any())
            {
                List<VoyageInfo> voyages = new()
                {
                    new VoyageInfo()
                    {
                        DepartureBusStopId = 1,
                        ArrivalBusStopId = 2,
                        DepartureDateTime = DateTime.Parse("12/25/2021 12:15:42"),
                        ArrivalDateTime = DateTime.Parse("12/25/2021 12:50:42"),
                        NumberOfSeats = 13,
                        OneTicketCost = 34,
                        TravelTimeMinutes = 21,
                        VoyageName = "Минский",
                        VoyageNumber = "A12",
                    },
                    new VoyageInfo()
                    {
                        DepartureBusStopId = 2,
                        ArrivalBusStopId = 3,
                        DepartureDateTime = DateTime.Parse("11/25/2021 12:15:42"),
                        ArrivalDateTime = DateTime.Parse("11/26/2021 12:50:42"),
                        NumberOfSeats = 13,
                        OneTicketCost = 34,
                        TravelTimeMinutes = 21,
                        VoyageName = "Минск-Гродно",
                        VoyageNumber = "969",
                    },
                    new VoyageInfo()
                    {
                        DepartureBusStopId = 4,
                        ArrivalBusStopId = 5,
                        DepartureDateTime = DateTime.Parse("11/3/2021 15:15:42"),
                        ArrivalDateTime = DateTime.Parse("11/3/2021 16:50:42"),
                        NumberOfSeats = 13,
                        OneTicketCost = 34,
                        TravelTimeMinutes = 21,
                        VoyageName = "Брест-Витебск",
                        VoyageNumber = "421",
                    },
                };

                busManagerContext.Voyages.AddRange(voyages);
                busManagerContext.SaveChanges();
            }
        }
    }
}
