using ONW_API.Domain.Enums;
using ONW_API.Domain.ValueObjects;

public sealed class Package
{
    public Guid Id { get; private set; }
    public Guid ShipmentId { get; private set; }
    public string TrackingCode { get; private set; } = null!;
    public Recipient Recipient { get; private set; }
    public PackageStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<PackageTrackingEvent> TrackingEvents { get; private set; } = new();

    protected Package() { }

    public Package(Guid shipmentId, Recipient recipient, Func<int> trackingNumberGenerator)
    {
        Id = Guid.NewGuid();
        ShipmentId = shipmentId;
        Recipient = recipient;
        Status = PackageStatus.Created;
        CreatedAt = DateTime.UtcNow;

        var number = trackingNumberGenerator();
        TrackingCode = $"ONWP-{DateTime.UtcNow.Year}-{number:D4}";

        AddEvent("Package created");
    }

    public void MarkOutForDelivery(Guid? driverId, Location location)
    {
        Status = PackageStatus.OutForDelivery;
        AddEvent("Package out for delivery", driverId, location);
    }

    public void Deliver(Guid? driverId, Location location)
    {
        Status = PackageStatus.Delivered;
        AddEvent("Package delivered", driverId, location);
    }

    private void AddEvent(string description, Guid? driverId = null, Location? location = null)
    {
        TrackingEvents.Add(new PackageTrackingEvent(
            Id,
            description,
            driverId,
            location
        ));
    }
}
