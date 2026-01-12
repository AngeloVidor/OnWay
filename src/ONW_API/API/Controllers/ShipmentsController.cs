using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.API.Requests;
using ONW_API.Application.DTOs;
using ONW_API.Application.Security;
using ONW_API.Application.Shipment;
using ONW_API.Application.Shipments;
using ONW_API.Application.UseCases;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public sealed class ShipmentsController : ControllerBase
    {
        private readonly CreateShipmentUseCase _createShipmentUseCase;
        private readonly GetRecentShipmentsUseCase _getRecentShipmentsUseCase;
        private readonly AssignVehicleUseCase _assignVehicleUseCase;
        private readonly AssignDriverUseCase _assignDriverUseCase;
        private readonly AssignDriverAndVehicleUseCase _assignDriverAndVehicleUseCase;
        private readonly GetActiveShipmentsUseCase _getActiveShipmentsUseCase;
        private readonly GetShipmentDetailsUseCase _getShipmentDetailsUseCase;


        public ShipmentsController(CreateShipmentUseCase createShipmentUseCase,
        AssignVehicleUseCase assignVehicleUseCase, AssignDriverUseCase assignDriverUseCase,
        AssignDriverAndVehicleUseCase assignDriverAndVehicleUseCase,
        GetRecentShipmentsUseCase getRecentShipmentsUseCase, GetActiveShipmentsUseCase getActiveShipmentsUseCase, GetShipmentDetailsUseCase getShipmentDetailsUseCase)

        {
            _createShipmentUseCase = createShipmentUseCase;
            _assignVehicleUseCase = assignVehicleUseCase;
            _assignDriverUseCase = assignDriverUseCase;
            _assignDriverAndVehicleUseCase = assignDriverAndVehicleUseCase;

            _getRecentShipmentsUseCase = getRecentShipmentsUseCase;
            _getActiveShipmentsUseCase = getActiveShipmentsUseCase;
            _getShipmentDetailsUseCase = getShipmentDetailsUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentRequest request)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            if (transporterId == Guid.Empty)
                return Unauthorized();

            var shipment = await _createShipmentUseCase.ExecuteAsync(
                transporterId,
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


        [HttpGet("active")]
        public async Task<ActionResult<List<ActiveShipmentDto>>> GetActiveShipments([FromQuery] int year, [FromQuery] int month)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            if (transporterId == Guid.Empty)
                return Unauthorized();

            var shipments = await _getActiveShipmentsUseCase.ExecuteAsync(transporterId, year, month);
            return Ok(shipments);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentShipments([FromQuery] int limit = 10)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            if (transporterId == Guid.Empty)
                return Unauthorized();

            var result = await _getRecentShipmentsUseCase
                .ExecuteAsync(transporterId, limit);

            return Ok(result);
        }

        [HttpGet("{shipmentId:guid}")]
        public async Task<IActionResult> GetShipmentDetails(Guid shipmentId)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            if (transporterId == Guid.Empty)
                return Unauthorized();

            var command = new GetShipmentDetailsCommand(shipmentId);

            var result = await _getShipmentDetailsUseCase.ExecuteAsync(command, transporterId);

            if (result is null || !result.Any())
                return NotFound();

            return Ok(result);
        }


    }
}