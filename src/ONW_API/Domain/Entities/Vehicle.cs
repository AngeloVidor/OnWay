using System;
using System.Data;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class Vehicle
    {
        public Guid Id { get; private set; }
        public string Plate { get; private set; } = null!;
        public string Model { get; private set; } = null!;
        public VehicleStatus Status { get; private set; }
        public Guid TransporterId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Vehicle() { }

        public Vehicle(string plate, string model, Guid transporterId, VehicleStatus status)
        {
            if (string.IsNullOrWhiteSpace(plate))
                throw new ArgumentException("Vehicle plate is required");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Vehicle model is required");

            Id = Guid.NewGuid();
            Plate = plate.Trim();
            Model = model.Trim();
            TransporterId = transporterId;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(VehicleStatus newStatus)
        {
            Status = newStatus;
        }


    }
}
