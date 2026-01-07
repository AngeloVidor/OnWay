using System;
using System.Collections.Generic;
using ONW_API.Domain.ValueObjects;
using OnWay.Domain.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class Shipment
    {
        public Guid Id { get; private set; }
        public string TrackingCode { get; private set; } = null!;
        public Guid TransporterId { get; private set; }

        public Location Origin { get; private set; }
        public Location Destination { get; private set; }
        public DateTime PickupDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public string? Notes { get; private set; }

        public Guid? DriverId { get; private set; }
        public List<Product> Products { get; private set; } = new();

        public ShipmentStatus Status { get; private set; } = ShipmentStatus.Pending;
        public List<ShipmentTrackingEvent> TrackingEvents { get; private set; } = new();

        public DateTime CreatedAt { get; private set; }

        private Shipment() { }

        public Shipment(Guid transporterId, Location origin, Location destination,
            DateTime pickupDate, DateTime estimatedDeliveryDate, string? notes, List<Product> products,
            Func<int> trackingNumberGenerator)
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
            Status = ShipmentStatus.Pending;

            var number = trackingNumberGenerator();
            TrackingCode = $"ONW-{CreatedAt.Year}-{number:D3}";

            TrackingEvents.Add(new ShipmentTrackingEvent(
                Guid.NewGuid(),
                CreatedAt,
                $"{Origin.City}, {Origin.State}",
                "Order created / Shipment registered"
            ));
        }

        public void AssignDriver(Guid driverId)
        {
            DriverId = driverId;

            TrackingEvents.Add(new ShipmentTrackingEvent(
                Guid.NewGuid(),
                DateTime.UtcNow,
                $"{Origin.City}, {Origin.State}",
                $"Driver assigned: {driverId}"
            ));
        }

        public void UpdateStatus(ShipmentStatus status)
        {
            Status = status;

            TrackingEvents.Add(new ShipmentTrackingEvent(
                Guid.NewGuid(),
                DateTime.UtcNow,
                $"{Origin.City}, {Origin.State}",
                $"Status updated to {status}"
            ));
        }
    }
}
