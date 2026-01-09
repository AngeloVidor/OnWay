public sealed class PackageTrackingEvent
{
    public Guid Id { get; private set; }
    public Guid PackageId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Description { get; private set; }
    public Guid? DriverId { get; private set; }
    public Location? Location { get; private set; }

    protected PackageTrackingEvent() { }

    public PackageTrackingEvent(Guid packageId, string description, Guid? driverId = null, Location? location = null)
    {
        Id = Guid.NewGuid();
        PackageId = packageId;
        Description = description;
        DriverId = driverId;
        Location = location;
        CreatedAt = DateTime.UtcNow;
    }
}
