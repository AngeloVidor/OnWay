using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Deliveries
{
    public sealed class StartRouteResult
    {
        public Guid ShipmentId { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public Guid DriverId { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public Guid VehicleId { get; set; }
    }
}