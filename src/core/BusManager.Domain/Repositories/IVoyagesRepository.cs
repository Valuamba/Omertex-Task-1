using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Repositories
{
    public interface IVoyagesRepository
    {
        Task<VoyageInfo[]> GetVoyages(int pageNumber, int pageSize, string from = null, string to = null, DateTime? departureTime = null, string voyageName = null);

        Task<VoyageInfo[]> GetVoyages();

        Task<VoyageInfo> GetVoyageByIdAsync(int voyageId);
    }
}
