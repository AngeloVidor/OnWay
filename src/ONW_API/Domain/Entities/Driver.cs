using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

public sealed class Driver
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DriverStatus Status { get; private set; }
    public PhoneNumber Phone { get; private set; }

    public Guid TransporterId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Driver() { }

    private Driver(string name, PhoneNumber phone, Guid transporterId)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Phone = phone;
        TransporterId = transporterId;
        Status = DriverStatus.Available;
        CreatedAt = DateTime.UtcNow;
    }

    public static Driver Create(string name, PhoneNumber phone, Guid transporterId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Driver name is required");

        return new Driver(name, phone, transporterId);
    }

    public void UpdateStatus(DriverStatus newStatus)
    {
        Status = newStatus;
    }

    public void Waiting()
    {
        Status = DriverStatus.Waiting;
    }

    public void FinishShipment()
    {
        Status = DriverStatus.Available;
    }
}
