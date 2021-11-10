using BusManager.Application.Contracts.Voyage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services.Interfaces
{
    public interface IVoyageService
    {
        Task<VoyageInfoRequest[]> SearchVoyages(string from = null, string to = null, DateTime? departureTime = null, string voyageName = null);

        Task<VoyageInfoRequest> GetVoyage(int voyageId);

        Task<VoyageInfoRequest[]> GetVoyages();
    }
}
