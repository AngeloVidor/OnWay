using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.API.DTOs;

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
            var shipments = await _shipmentRepository
                .GetRecentShipmentsAsync(transporterId, limit);

            var result = shipments.Select(s => new RecentShipmentDto
            {
                TrackingCode = s.TrackingCode,
                Status = s.Status.ToString(),
                Route = $"{s.Origin.City}, {s.Origin.State} → {s.Destination.City}, {s.Destination.State}",
                Products = s.Products.Select(p => new ProductDto
                {
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Weight = (double)p.Weight,
                    Status = s.Status.ToString()
                }).ToList(),
                TrackingHistory = s.TrackingEvents.Select(t => new TrackingEventDto
                {
                    Date = t.Date,
                    Location = t.Location,
                    Description = t.Description
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
