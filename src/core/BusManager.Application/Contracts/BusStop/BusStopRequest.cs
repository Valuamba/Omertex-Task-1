namespace BusManager.Application.Contracts.BusStop
{
    public class BusStopRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
