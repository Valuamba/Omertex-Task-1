using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int VoyageId { get; set; }
        public VoyageInfo Voyage { get; set; }

        public OrderStatus Status { get; set; }

        public Ticket Ticket { get; set; }
    }
}
