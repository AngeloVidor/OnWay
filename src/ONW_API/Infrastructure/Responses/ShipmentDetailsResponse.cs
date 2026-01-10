using ONW_API.Domain.ValueObjects;

namespace ONW_API.Infrastructure.Responses
{
    public sealed class ShipmentDetailsResponse
    {
        public Guid id { get; init; }
        public string tracking_code { get; init; } = null!;
        public Guid transporter_id { get; init; }
        public Guid vehicle_id { get; init; }
        public Guid? driver_id { get; init; }

        public Location origin { get; init; } = null!;
        public Location destination { get; init; } = null!;

        public ShipmentStatus status { get; init; }
        public DateTime created_at { get; init; }

        public List<PackageResponse> packages { get; init; } = new();
    }
}
