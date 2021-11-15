namespace BusManager.Application.Contracts.Order
{
    public class ProcessOrderResponse
    {
        public int TicketId { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
