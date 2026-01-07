using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;
using OnWay.Domain.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class Shipment
    {
        public Guid Id { get; private set; }
        public Guid TransporterId { get; private set; }

        public Location Origin { get; private set; }
        public Location Destination { get; private set; }
        public DateTime PickupDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public string? Notes { get; private set; }

        public Guid? DriverId { get; private set; }
        public List<Product> Products { get; private set; } = new();

        public DateTime CreatedAt { get; private set; }

        private Shipment() { }

        public Shipment(Guid transporterId, Location origin, Location destination,
            DateTime pickupDate, DateTime estimatedDeliveryDate, string? notes, List<Product> products)
        {
            Id = Guid.NewGuid();
            TransporterId = transporterId;
            Origin = origin;
            Destination = destination;
            PickupDate = pickupDate;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Notes = notes;
            Products = products ?? new List<Product>();
            CreatedAt = DateTime.UtcNow;
        }

        public void AssignDriver(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}