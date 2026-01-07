using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipment
{
    public class GetShipmentsByStatusRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}