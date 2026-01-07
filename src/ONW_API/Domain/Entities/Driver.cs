using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.Entities;

public sealed class Driver
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DriverStatus Status { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public string Vehicle { get; private set; }
    public string VehiclePlate { get; private set; }
    public Guid TransporterId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Driver() { }

    private Driver(string name, PhoneNumber phone, string vehicle, string plate, Guid transporterId)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Phone = phone;
        Vehicle = vehicle.Trim();
        VehiclePlate = plate.Trim();
        Status = DriverStatus.Available;
        TransporterId = transporterId;
        CreatedAt = DateTime.UtcNow;
    }

    public static Driver Create(string name, PhoneNumber phone, string vehicle, string plate, Guid transporterId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome do motorista é obrigatório");
        if (string.IsNullOrWhiteSpace(vehicle)) throw new ArgumentException("Veículo é obrigatório");
        if (string.IsNullOrWhiteSpace(plate)) throw new ArgumentException("Placa do veículo é obrigatória");

        return new Driver(name, phone, vehicle, plate, transporterId);
    }

    public void SetStatus(DriverStatus status) => Status = status;
}
