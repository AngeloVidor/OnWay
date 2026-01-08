using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.DTOs;
using ONW_API.Application.Security;
using ONW_API.Application.Shipment;
using ONW_API.Application.UseCases;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public sealed class ShipmentsController : ControllerBase
    {
        private readonly CreateShipmentUseCase _createShipmentUseCase;
        //private readonly UpdateShipmentStatusUseCase _updateShipmentStatusUseCase;
        //private readonly GetShipmentsByStatusUseCase _getShipmentsByStatusUseCase;
        //private readonly GetRecentShipmentsUseCase _getRecentShipmentsUseCase;
        private readonly AssignVehicleUseCase _assignVehicleUseCase;
        private readonly AssignDriverUseCase _assignDriverUseCase;
        private readonly AssignDriverAndVehicleUseCase _assignDriverAndVehicleUseCase;




        public ShipmentsController(CreateShipmentUseCase createShipmentUseCase, AssignVehicleUseCase assignVehicleUseCase, AssignDriverUseCase assignDriverUseCase, AssignDriverAndVehicleUseCase assignDriverAndVehicleUseCase)
        //UpdateShipmentStatusUseCase updateShipmentStatusUseCase, GetShipmentsByStatusUseCase getShipmentsByStatusUseCase, GetRecentShipmentsUseCase getRecentShipmentsUseCase)
        {
            _createShipmentUseCase = createShipmentUseCase;
            _assignVehicleUseCase = assignVehicleUseCase;
            _assignDriverUseCase = assignDriverUseCase;
            _assignDriverAndVehicleUseCase = assignDriverAndVehicleUseCase;
            //_updateShipmentStatusUseCase = updateShipmentStatusUseCase;
            //_getShipmentsByStatusUseCase = getShipmentsByStatusUseCase;
            //_getRecentShipmentsUseCase = getRecentShipmentsUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentRequest request)
        {
            var transporterId = ClaimsHelper.GetUserId(User);

            var shipment = await _createShipmentUseCase.ExecuteAsync(
                transporterId,
                //request.VehicleId,
                request.Origin,
                request.Destination
            );

            return Ok(new
            {
                shipment.Id,
                shipment.TrackingCode
            });
        }

        [HttpPost("{shipmentId}/assign-driver")]
        public async Task<IActionResult> AssignDriver(Guid shipmentId, [FromBody] AssignDriverCommand command)
        {
            command = command with { ShipmentId = shipmentId };
            var shipment = await _assignDriverUseCase.ExecuteAsync(command);
            return Ok(new { shipment.Id, shipment.DriverId });
        }

        [HttpPost("{shipmentId}/assign-vehicle")]
        public async Task<IActionResult> AssignVehicle(Guid shipmentId, [FromBody] AssignVehicleCommand command)
        {
            command = command with { ShipmentId = shipmentId };
            var shipment = await _assignVehicleUseCase.ExecuteAsync(command);
            return Ok(new { shipment.Id, shipment.VehicleId });
        }

        [HttpPost("{shipmentId}/assign-driver-vehicle")]
        public async Task<IActionResult> AssignDriverAndVehicle(Guid shipmentId, [FromBody] AssignDriverAndVehicleCommand command)
        {
            command = command with { ShipmentId = shipmentId };
            var shipment = await _assignDriverAndVehicleUseCase.ExecuteAsync(command);
            return Ok(new { shipment.Id, shipment.DriverId, shipment.VehicleId });
        }

    }

    //     [HttpPost("{shipmentId}/assign-driver")]
    //     public async Task<IActionResult> AssignDriver(Guid shipmentId, [FromBody] AssignDriverCommand command)
    //     {
    //         await _useCase.AssignDriverAsync(shipmentId, command.DriverId);
    //         return NoContent();
    //     }

    //     [HttpPatch("{id}/status")]
    //     public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] ShipmentStatus newStatus)
    //     {
    //         var request = new UpdateShipmentStatusRequest
    //         {
    //             ShipmentId = id,
    //             NewStatus = newStatus
    //         };

    //         await _updateShipmentStatusUseCase.ExecuteAsync(request);
    //         return NoContent();
    //     }

    //     [HttpGet("active")]
    //     public async Task<IActionResult> GetActiveShipments([FromQuery] int year, [FromQuery] int month)
    //     {
    //         var transporterId = ClaimsHelper.GetUserId(User);

    //         var request = new GetShipmentsByStatusRequest
    //         {
    //             Year = year,
    //             Month = month,
    //             TransporterId = transporterId
    //         };

    //         var result = await _getShipmentsByStatusUseCase
    //             .ExecuteAsync(ShipmentStatus.Pending, request);

    //         return Ok(result);
    //     }


    //     [HttpGet("in-transit")]
    //     public async Task<IActionResult> GetInTransitShipments([FromQuery] int year, [FromQuery] int month)
    //     {
    //         var transporterId = ClaimsHelper.GetUserId(User);

    //         var request = new GetShipmentsByStatusRequest
    //         {
    //             TransporterId = transporterId,
    //             Year = year,
    //             Month = month
    //         };

    //         var result = await _getShipmentsByStatusUseCase
    //             .ExecuteAsync(ShipmentStatus.InTransit, request);

    //         return Ok(result);
    //     }


    //     [HttpGet("delivered")]
    //     public async Task<IActionResult> GetDeliveredShipments([FromQuery] int year, [FromQuery] int month)
    //     {
    //         var transporterId = ClaimsHelper.GetUserId(User);

    //         var request = new GetShipmentsByStatusRequest
    //         {
    //             TransporterId = transporterId,
    //             Year = year,
    //             Month = month
    //         };

    //         var result = await _getShipmentsByStatusUseCase
    //             .ExecuteAsync(ShipmentStatus.Delivered, request);

    //         return Ok(result);
    //     }


    //     [HttpGet("delayed")]
    //     public async Task<IActionResult> GetDelayedShipments([FromQuery] int year, [FromQuery] int month)
    //     {
    //         var transporterId = ClaimsHelper.GetUserId(User);

    //         var request = new GetShipmentsByStatusRequest
    //         {
    //             TransporterId = transporterId,
    //             Year = year,
    //             Month = month
    //         };

    //         var result = await _getShipmentsByStatusUseCase
    //             .ExecuteAsync(ShipmentStatus.Delayed, request);

    //         return Ok(result);
    //     }


    //     [HttpGet("recent")]
    //     public async Task<IActionResult> GetRecentShipments([FromQuery] int limit = 10)
    //     {
    //         var transporterId = ClaimsHelper.GetUserId(User);

    //         var result = await _getRecentShipmentsUseCase
    //             .ExecuteAsync(transporterId, limit);

    //         return Ok(result);
    //     }

    // }

}