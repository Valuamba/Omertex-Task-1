using AutoMapper;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Repositories
{
    public class BusStopRepository : IBusStopRepository
    {
        private readonly MssqlBusManagerDbContext _context;
        private readonly IMapper _mapper;

        public BusStopRepository(MssqlBusManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BusStop[]> GetBusStops()
        {
            var busStops = await _context.BusStops
                   .AsNoTracking()
                   .ToArrayAsync();

            return _mapper.Map<Entities.BusStop[], BusStop[]>(busStops);
        }
    }
}
