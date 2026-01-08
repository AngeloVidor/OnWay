// using Microsoft.EntityFrameworkCore;
// using ONW_API.Domain.Repositories;
// using ONW_API.Domain.ValueObjects;
// using OnWay.API.Domain.Entities;
// using OnWay.Domain.ValueObjects;

// namespace ONW_API.Application.Shipment;

// public sealed class CreateShipmentUseCase
// {
//     private readonly IShipmentRepository _shipmentRepository;

//     public CreateShipmentUseCase(IShipmentRepository shipmentRepository)
//     {
//         _shipmentRepository = shipmentRepository;
//     }

//     public async Task<Guid> ExecuteAsync(CreateShipmentCommand command, Guid transporterId)
//     {
//         var origin = new Location(command.OriginAddress, command.OriginCity, command.OriginState);
//         var destination = new Location(command.DestinationAddress, command.DestinationCity, command.DestinationState);

//         var products = command.Products.Select(p => new Product(p.Name, p.Quantity, p.Weight)).ToList();

//         int nextNumber = await _shipmentRepository.GetNextTrackingNumberAsync(DateTime.UtcNow.Year);

//         var shipment = new Domain.Entities.Shipment(
//             transporterId,
//             origin,
//             destination,
//             command.PickupDate,
//             command.EstimatedDeliveryDate,
//             command.Notes,
//             products,
//             () => nextNumber
//         );

//         await _shipmentRepository.AddAsync(shipment);
//         await _shipmentRepository.SaveChangesAsync();

//         return shipment.Id;
//     }

//     public async Task AssignDriverAsync(Guid shipmentId, Guid driverId)
//     {
//         var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);
//         if (shipment == null) throw new InvalidOperationException("Shipment não encontrado");

//         shipment.AssignDriver(driverId);
//         //_shipmentRepository.Update(shipment);

//         try
//         {
//             await _shipmentRepository.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException)
//         {
//             throw new InvalidOperationException("Falha ao atribuir motorista: Shipment foi alterado ou removido.");
//         }
//     }
// }
