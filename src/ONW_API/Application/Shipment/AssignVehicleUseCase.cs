using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;

namespace ONW_API.Application.Shipment
{
    public sealed class AssignVehicleUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public AssignVehicleUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Domain.Entities.Shipment> ExecuteAsync(AssignVehicleCommand command)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(command.ShipmentId);
            if (shipment == null) throw new Exception("Shipment not found");

            shipment.AssignVehicle(command.VehicleId);

            _shipmentRepository.Update(shipment);
            await _shipmentRepository.SaveChangesAsync();

            return shipment;
        }
    }
}
