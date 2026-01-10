using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Responses;
using OnWay.Infrastructure.Repositories;

namespace ONW_API.Application.Shipments
{
    public sealed class GetShipmentDetailsUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentDetailsUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<List<ShipmentDetailsResponse>> ExecuteAsync(GetShipmentDetailsCommand command, Guid transporterId)
        {
            return await _shipmentRepository.GetShipmentDetailsAsync(command.ShipmentId, transporterId);
        }
    }
}
