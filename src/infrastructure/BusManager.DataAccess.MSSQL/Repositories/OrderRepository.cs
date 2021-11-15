using AutoMapper;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MssqlBusManagerDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(MssqlBusManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<Order> GetOrdersByUserId(int userId)
        {
            var orders = _context.Orders.Include(o => o.Ticket)
                .Include(o => o.Voyage).ThenInclude(v => v.ArrivalBusStop)
                .Include(o => o.Voyage).ThenInclude(v => v.DepartureBusStop)
                .Where(o => o.UserId == userId).AsNoTracking();

            return orders.Select(o => _mapper.Map<Entities.Order, Order>(o)).ToList();
        }

        public IList<Order> GetOrdersByVoyageIdAsync(int voyageId)
        {
            var orders = _context.Orders.Include(o => o.Ticket).Where(o => o.VoyageId == voyageId).AsNoTracking();

            return orders.Select(o => _mapper.Map<Entities.Order, Order>(o)).ToList();
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            var order = _mapper.Map<Order, Entities.Order>(newOrder);

            var trackedEntity = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<Entities.Order, Order>(trackedEntity.Entity);
        }

        public async Task UpdateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var orderEntity = _mapper.Map<Order, Entities.Order>(order);

            var trackedOrder = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (trackedOrder == null)
            {
                throw new ArgumentNullException(nameof(trackedOrder));
            }

            trackedOrder.Status = orderEntity.Status;

            await _context.SaveChangesAsync();
        }
    }
}
