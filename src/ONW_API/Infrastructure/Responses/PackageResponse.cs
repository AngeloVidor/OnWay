using ONW_API.Domain.Enums;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Infrastructure.Responses
{
    public sealed class PackageResponse
    {
        public Guid id { get; init; }
        public string tracking_code { get; init; } = null!;
        public PackageStatus status { get; init; }
        public DateTime created_at { get; init; }

        public Recipient recipient { get; init; } = null!;
    }
}
