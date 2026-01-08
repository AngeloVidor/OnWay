using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Shipment
{
    public sealed class AssignDriverAndVehicleUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public AssignDriverAndVehicleUseCase(IShipmentRepository shipmentRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _shipmentRepository = shipmentRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Domain.Entities.Shipment> ExecuteAsync(AssignDriverAndVehicleCommand command)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(command.ShipmentId);
            if (shipment == null) throw new Exception("Shipment not found");

            var driver = await _driverRepository.GetByIdAsync(command.DriverId);
            if (driver == null) throw new Exception("Driver not found");

            var vehicle = await _vehicleRepository.GetByIdAsync(command.VehicleId);
            if (vehicle == null) throw new Exception("Vehicle not found");

            driver.AssignToShipment();
            shipment.AssignDriver(command.DriverId);
            shipment.AssignVehicle(command.VehicleId);
            vehicle.UpdateStatus(VehicleStatus.OnRoute);

            _vehicleRepository.Update(vehicle);
            _driverRepository.Update(driver);
            _shipmentRepository.Update(shipment);

            await _driverRepository.SaveChangesAsync();
            await _shipmentRepository.SaveChangesAsync();
            await _vehicleRepository.SaveChangesAsync();

            return shipment;
        }
    }
}
