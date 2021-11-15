﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Models
{
    public class BusStop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public ICollection<VoyageInfo> DepartureBusStopVoyages { get; set; }

        public ICollection<VoyageInfo> ArrivalBusStopVoyages { get; set; }
    }
}
