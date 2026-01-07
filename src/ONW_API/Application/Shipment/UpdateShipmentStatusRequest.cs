using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Shipment
{
    public class UpdateShipmentStatusRequest
    {
        public Guid ShipmentId { get; set; }
        public ShipmentStatus NewStatus { get; set; }
    }
}