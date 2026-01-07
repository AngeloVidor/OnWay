using System;
using System.Collections.Generic;

namespace ONW_API.Application.Shipment
{
    public sealed class CreateShipmentCommand
    {
        public string OriginAddress { get; set; } = null!;
        public string OriginCity { get; set; } = null!;
        public string OriginState { get; set; } = null!;

        public string DestinationAddress { get; set; } = null!;
        public string DestinationCity { get; set; } = null!;
        public string DestinationState { get; set; } = null!;

        public DateTime PickupDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        public string? Notes { get; set; }

        public List<ProductCommand> Products { get; set; } = new();
    }
}