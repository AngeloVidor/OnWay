using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Shipment
{
    public class GetShipmentsByStatusUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentsByStatusUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<List<Domain.Entities.Shipment>> ExecuteAsync(ShipmentStatus status, GetShipmentsByStatusRequest request)
        {
            if (status == ShipmentStatus.Pending || status == ShipmentStatus.InTransit)
            {
                return await _shipmentRepository.GetActiveShipmentsAsync(request.Year, request.Month);
            }

            return await _shipmentRepository.GetShipmentsByStatusAndMonthAsync(status, request.Year, request.Month);
        }
    }
}