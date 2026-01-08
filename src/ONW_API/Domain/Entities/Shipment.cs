using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class Shipment
    {
        public Guid Id { get; private set; }
        public string TrackingCode { get; private set; } = null!;
        public Guid TransporterId { get; private set; }

        public Guid VehicleId { get; private set; }
        public Guid? DriverId { get; private set; }

        public Location Origin { get; private set; }
        public Location Destination { get; private set; }

        public ShipmentStatus Status { get; private set; }
        public List<Package> Packages { get; private set; } = new();

        public DateTime CreatedAt { get; private set; }

        private Shipment() { }

        public Shipment(
            Guid transporterId,
            Location origin,
            Location destination,
            Func<int> trackingNumberGenerator)
        {
            Id = Guid.NewGuid();
            TransporterId = transporterId;
            Origin = origin;
            Destination = destination;

            CreatedAt = DateTime.UtcNow;
            Status = ShipmentStatus.Created;

            var number = trackingNumberGenerator();
            TrackingCode = $"ONW-{CreatedAt.Year}-{number:D3}";
        }

        public void UpdateStatus(ShipmentStatus newStatus)
        {
            Status = newStatus;
        }

        public void AssignDriver(Guid driverId)
        {
            DriverId = driverId;
        }

        public void AssignVehicle(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        public void ChangeVehicle(Guid newVehicleId)
        {
            VehicleId = newVehicleId;
        }

        public void AddPackage(Package package)
        {
            Packages.Add(package);
        }
    }
}
