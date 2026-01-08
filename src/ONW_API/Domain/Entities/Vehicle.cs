using System;

namespace ONW_API.Domain.Entities
{
    public sealed class Vehicle
    {
        public Guid Id { get; private set; }
        public string Plate { get; private set; } = null!;
        public string Model { get; private set; } = null!;
        public Guid TransporterId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Vehicle() { }

        public Vehicle(string plate, string model, Guid transporterId)
        {
            if (string.IsNullOrWhiteSpace(plate))
                throw new ArgumentException("Placa do veículo é obrigatória");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Modelo do veículo é obrigatório");

            Id = Guid.NewGuid();
            Plate = plate.Trim();
            Model = model.Trim();
            TransporterId = transporterId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
