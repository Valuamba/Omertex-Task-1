namespace BusManager.Application.Contracts.Ticket
{
    public class BuyTicketResponse
    {
        public int TicketId { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
