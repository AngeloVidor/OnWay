using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.API.DTOs;
using ONW_API.API.Requests;

namespace ONW_API.Application.Shipments
{
    public class GetRecentShipmentsUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetRecentShipmentsUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<List<RecentShipmentDto>> ExecuteAsync(Guid transporterId, int limit = 10)
        {
            var shipments = await _shipmentRepository.GetRecentShipmentsAsync(transporterId, limit);

            var result = shipments.Select(s => new RecentShipmentDto
            {
                TrackingCode = s.TrackingCode,
                Status = s.Status.ToString(),
                Route = $"{s.Origin.City}, {s.Origin.State} → {s.Destination.City}, {s.Destination.State}",
                Packages = s.Packages.Select(p => new PackageDto
                {
                    TrackingCode = p.TrackingCode,
                    RecipientName = p.Recipient.Name,
                    RecipientEmail = p.Recipient.Email.Value,
                    Status = p.Status.ToString()
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
