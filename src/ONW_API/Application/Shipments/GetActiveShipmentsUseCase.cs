using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.API.DTOs;
using ONW_API.API.Requests;

namespace ONW_API.Application.Shipments
{
    public class GetActiveShipmentsUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetActiveShipmentsUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<List<ActiveShipmentDto>> ExecuteAsync(Guid transporterId, int year, int month)
        {
            var shipments = await _shipmentRepository.GetActiveShipmentsAsync(transporterId, year, month);

            var result = shipments.Select(s => new ActiveShipmentDto
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
