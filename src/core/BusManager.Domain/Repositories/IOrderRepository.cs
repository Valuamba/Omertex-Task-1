using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Repositories
{
    public interface IOrderRepository
    {
        IList<Order> GetOrdersByUserId(int userId);
        Task<Order> AddOrder(Order newOrder);
        IList<Order> GetOrdersByVoyageIdAsync(int voyageId);
        Task UpdateOrder(Order order);
    }
}
