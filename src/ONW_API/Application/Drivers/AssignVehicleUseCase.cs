// using System;
// using System.Threading.Tasks;
// using ONW_API.Domain.Entities;
// using ONW_API.Domain.Repositories;

// namespace ONW_API.Application.Drivers
// {
//     public sealed class AssignVehicleUseCase
//     {
//         private readonly IDriverRepository _driverRepository;
//         private readonly IVehicleRepository _vehicleRepository;

//         public AssignVehicleUseCase(IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
//         {
//             _driverRepository = driverRepository;
//             _vehicleRepository = vehicleRepository;
//         }

//         public async Task ExecuteAsync(Guid driverId, Guid vehicleId)
//         {
//             var driver = await _driverRepository.GetByIdAsync(driverId);
//             if (driver == null)
//                 throw new ArgumentException("Motorista não encontrado");

//             var vehicleExists = await _vehicleRepository.ExistsAsync(vehicleId);
//             if (!vehicleExists)
//                 throw new ArgumentException("Veículo não encontrado");

//             driver.AssignVehicle(vehicleId);
//             await _driverRepository.SaveChangesAsync();
//         }
//     }
// }
