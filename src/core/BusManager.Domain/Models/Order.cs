using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Models
{
    public class Order
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int OrderId { get; set; }

        public int VoyageId { get; set; }
        public VoyageInfo Voyage { get; set; }

        public OrderStatus Status { get; set; }

        public Ticket Ticket { get; set; }
    }

    public enum OrderStatus
    {
        Reserved = 0,
        BoughtOut = 2
    }
}
