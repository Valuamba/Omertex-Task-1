using AutoMapper;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Repositories
{
    public class VoyagesRepository : IVoyagesRepository
    {
        private readonly MssqlBusManagerDbContext _context;
        private readonly IMapper _mapper;

        public VoyagesRepository(MssqlBusManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VoyageInfo[]> GetVoyages(int pageNumber, int pageSize, string from = null, string to = null, DateTime? departureTime = null, string voyageName = null)
        {
            IQueryable<Entities.VoyageInfo> result = _context.Voyages
                .Include(v => v.DepartureBusStop).Include(v => v.ArrivalBusStop)
                .Include(v => v.Orders).ThenInclude(o => o.Ticket);

            if (!string.IsNullOrEmpty(from))
                result = result.Where(x => x.DepartureBusStop.Name.ToLower().Contains(from.ToLower()));

            if (!string.IsNullOrEmpty(to))
                result = result.Where(x => x.ArrivalBusStop.Name.ToLower().Contains(to.ToLower()));

            if (departureTime.HasValue)
                result = result.Where(x => x.DepartureDateTime.Date.CompareTo(departureTime.Value.Date) == 0);

            if (!string.IsNullOrEmpty(voyageName))
                result = result.Where(x => x.VoyageName.ToLower().Contains(voyageName.ToLower()));

            var voyages = await result
                       .Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize)
                       .AsNoTracking()
                       .ToArrayAsync();

            return _mapper.Map<Entities.VoyageInfo[], VoyageInfo[]>(voyages);

        }

        public async Task<VoyageInfo[]> GetVoyages()
        {
            var voyages = await _context.Voyages.Include(v => v.DepartureBusStop).Include(v => v.ArrivalBusStop)
                   .Include(v => v.Orders).ThenInclude(o => o.Ticket)
                   .AsNoTracking()
                   .ToArrayAsync();

            return _mapper.Map<Entities.VoyageInfo[], VoyageInfo[]>(voyages);
        }

        public async Task<VoyageInfo> GetVoyageByIdAsync(int voyageId)
        {
            var voyage = await _context.Voyages
                .Include(v => v.DepartureBusStop).Include(v => v.ArrivalBusStop)
                .Include(v => v.Orders).ThenInclude(o => o.Ticket)
                .FirstAsync(v => v.Id == voyageId);

            return _mapper.Map<Entities.VoyageInfo, VoyageInfo>(voyage);
        }
    }
}
