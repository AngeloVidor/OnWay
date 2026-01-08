// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using ONW_API.Domain.Repositories;

// namespace ONW_API.Application.Shipment
// {
//     public class UpdateShipmentStatusUseCase
//     {
//         private readonly IShipmentRepository _shipmentRepository;

//         public UpdateShipmentStatusUseCase(IShipmentRepository shipmentRepository)
//         {
//             _shipmentRepository = shipmentRepository;
//         }

//         public async Task ExecuteAsync(UpdateShipmentStatusRequest request)
//         {
//             var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId);
//             if (shipment == null)
//                 throw new InvalidOperationException("Shipment não encontrado");

//             shipment.UpdateStatus(request.NewStatus);

//             try
//             {
//                 await _shipmentRepository.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 throw new InvalidOperationException("Falha ao atualizar status: Shipment foi alterado ou removido.");
//             }
//         }
//     }
// }