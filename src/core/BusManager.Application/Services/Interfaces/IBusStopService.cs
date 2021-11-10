using BusManager.Application.Contracts.BusStop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services.Interfaces
{
    public interface IBusStopService
    {
        Task<BusStopRequest[]> GetBusStops();
    }
}
