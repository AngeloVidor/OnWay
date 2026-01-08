using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Deliveries
{
    public sealed class ShipmentDriverVehicleDto
    {
        public Guid ShipmentId { get; set; }
        public string TrackingCode { get; set; } = null!;
        public ShipmentStatus ShipmentStatus { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
    }
}