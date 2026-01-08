// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ONW_API.Domain.Entities;
// using ONW_API.Domain.Repositories;
// using OnWay.Domain.Transporters.ValueObjects;

// namespace ONW_API.Application.Drivers
// {
//     public sealed class CreateDriverUseCase
//     {
//         private readonly IDriverRepository _driverRepository;

//         public CreateDriverUseCase(IDriverRepository driverRepository)
//         {
//             _driverRepository = driverRepository;
//         }

//         public async Task<Guid> ExecuteAsync(CreateDriverCommand command, Guid transporterId)
//         {
//             var driver = Driver.Create(
//                 command.Name,
//                 PhoneNumber.Create(command.Phone),
//                 command.Vehicle,
//                 command.VehiclePlate,
//                 transporterId
//             );

//             await _driverRepository.AddAsync(driver);
//             await _driverRepository.SaveChangesAsync();

//             return driver.Id;
//         }
//     }

// }