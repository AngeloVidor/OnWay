using System;
using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.Entities
{
    public sealed class Driver
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DriverStatus Status { get; private set; }
        public PhoneNumber Phone { get; private set; }

        public Guid VehicleId { get; private set; }

        public Guid TransporterId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private Driver() { }

        private Driver(string name, PhoneNumber phone, Guid vehicleId, Guid transporterId)
        {
            Id = Guid.NewGuid();
            Name = name.Trim();
            Phone = phone;
            VehicleId = vehicleId;
            TransporterId = transporterId;
            Status = DriverStatus.Available;
            CreatedAt = DateTime.UtcNow;
        }

        public static Driver Create(string name, PhoneNumber phone, Guid vehicleId, Guid transporterId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome do motorista é obrigatório");

            return new Driver(name, phone, vehicleId, transporterId);
        }

        public void SetStatus(DriverStatus status) => Status = status;

        public void AssignVehicle(Guid vehicleId) => VehicleId = vehicleId;
    }
}
