using BusManager.Application.Contracts.BusStop;
using BusManager.Application.Services.Interfaces;
using BusManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services.BusStop
{
    public class BusStopService : IBusStopService
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopService(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        public async Task<BusStopRequest[]> GetBusStops()
        {
            var busStops = await _busStopRepository.GetBusStops();

            return busStops.Select(b => new BusStopRequest()
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Status = b.Status
            }).ToArray();
        }
    }
}
