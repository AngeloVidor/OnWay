using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;

namespace ONW_API.Application.Shipment
{
    public sealed class AssignDriverUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IDriverRepository _driverRepository;

        public AssignDriverUseCase(IShipmentRepository shipmentRepository, IDriverRepository driverRepository)
        {
            _shipmentRepository = shipmentRepository;
            _driverRepository = driverRepository;
        }

        public async Task<Domain.Entities.Shipment> ExecuteAsync(AssignDriverCommand command)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(command.ShipmentId);
            if (shipment == null) throw new Exception("Shipment not found");

            var driver = await _driverRepository.GetByIdAsync(command.DriverId);
            if (driver == null) throw new Exception("Driver not found");

            driver.AssignToShipment();
            shipment.AssignDriver(command.DriverId);

            _driverRepository.Update(driver);
            _shipmentRepository.Update(shipment);

            await _driverRepository.SaveChangesAsync();
            await _shipmentRepository.SaveChangesAsync();

            return shipment;
        }
    }
}
