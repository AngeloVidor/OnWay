using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;

namespace ONW_API.Application.Shipment
{
    public class UpdateShipmentStatusUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public UpdateShipmentStatusUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task ExecuteAsync(UpdateShipmentStatusRequest request)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            shipment.UpdateStatus(request.NewStatus);

            _shipmentRepository.Update(shipment);
            await _shipmentRepository.SaveChangesAsync();
        }
    }
}