using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipment
{
    public sealed class AssignDriverCommand
    {
        public Guid ShipmentId { get; set; }
        public Guid DriverId { get; set; }
    }
}