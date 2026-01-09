using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ONW_API.Application.Deliveries
{
    public sealed class StartRouteUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public StartRouteUseCase(IShipmentRepository shipmentRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _shipmentRepository = shipmentRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<StartRouteResult> ExecuteAsync(Guid driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null)
                throw new InvalidOperationException($"Driver {driverId} not found.");

            var shipmentDto = await _shipmentRepository.GetShipmentWithVehicleByDriverAsync(driverId);
            if (shipmentDto == null)
                throw new InvalidOperationException($"No shipment assigned to driver {driverId}.");

            var shipment = await _shipmentRepository.GetByIdAsync(shipmentDto.ShipmentId);
            if (shipment == null) throw new Exception("Shipment not found");
            
            if (shipment.Status != ShipmentStatus.Created)
                throw new InvalidOperationException($"Shipment {shipment.Id} cannot start route because it is {shipment.Status}.");

            var vehicle = await _vehicleRepository.GetByIdAsync(shipmentDto.VehicleId);
            if (vehicle == null)
                throw new Exception("Vehicle not found");

            vehicle.UpdateStatus(VehicleStatus.OnRoute);
            shipment.UpdateStatus(ShipmentStatus.InTransit);
            driver.UpdateStatus(DriverStatus.OnRoute);

            _vehicleRepository.Update(vehicle);
            _shipmentRepository.Update(shipment);
            _driverRepository.Update(driver);

            await _vehicleRepository.SaveChangesAsync();
            await _shipmentRepository.SaveChangesAsync();
            await _driverRepository.SaveChangesAsync();

            return new StartRouteResult
            {
                ShipmentId = shipment.Id,
                ShipmentStatus = shipment.Status,
                DriverId = driver.Id,
                DriverStatus = driver.Status,
                VehicleId = shipment.VehicleId
            };
        }
    }

}
