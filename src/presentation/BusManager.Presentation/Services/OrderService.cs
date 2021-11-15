using BusManager.Application.Contracts.Order;
using System.Threading.Tasks;

namespace BusManager.Presentation.Services
{
    public interface IOrderService
    {
        Task<ProcessOrderResponse> CreateOrder(TicketOrder order);
    }

    public class OrderService : IOrderService
    {
        private IHttpService _httpService;

        public OrderService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ProcessOrderResponse> CreateOrder(TicketOrder order)
        {
            return await _httpService.Post<ProcessOrderResponse>("/api/order", order);
        }
    }
}
