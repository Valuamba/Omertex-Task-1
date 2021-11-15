using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services.Interfaces
{
    public interface IVoyageService
    {
        Task<VoyageInfoRequest> GetVoyage(int voyageId);

        Task<PagedList<VoyageInfoRequest>> GetVoyages(VoyageParameters voyageParameters);
    }
}
