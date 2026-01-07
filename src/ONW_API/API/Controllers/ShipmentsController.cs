using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Security;
using ONW_API.Application.Shipment;
using ONW_API.Application.Shipments;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public sealed class ShipmentsController : ControllerBase
    {
        private readonly CreateShipmentUseCase _useCase;
        private readonly UpdateShipmentStatusUseCase _updateShipmentStatusUseCase;
        private readonly GetShipmentsByStatusUseCase _getShipmentsByStatusUseCase;
        private readonly GetRecentShipmentsUseCase _getRecentShipmentsUseCase;




        public ShipmentsController(CreateShipmentUseCase useCase, UpdateShipmentStatusUseCase updateShipmentStatusUseCase, GetShipmentsByStatusUseCase getShipmentsByStatusUseCase, GetRecentShipmentsUseCase getRecentShipmentsUseCase)
        {
            _useCase = useCase;
            _updateShipmentStatusUseCase = updateShipmentStatusUseCase;
            _getShipmentsByStatusUseCase = getShipmentsByStatusUseCase;
            _getRecentShipmentsUseCase = getRecentShipmentsUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            if (transporterId == Guid.Empty)
            {
                return Unauthorized();
            }
            var shipmentId = await _useCase.ExecuteAsync(command, transporterId);

            return CreatedAtAction(nameof(Create), new { id = shipmentId }, new { id = shipmentId });
        }

        [HttpPost("{shipmentId}/assign-driver")]
        public async Task<IActionResult> AssignDriver(Guid shipmentId, [FromBody] AssignDriverCommand command)
        {
            await _useCase.AssignDriverAsync(shipmentId, command.DriverId);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] ShipmentStatus newStatus)
        {
            var request = new UpdateShipmentStatusRequest
            {
                ShipmentId = id,
                NewStatus = newStatus
            };

            await _updateShipmentStatusUseCase.ExecuteAsync(request);
            return NoContent();
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveShipments([FromQuery] int year, [FromQuery] int month)
        {
            var request = new GetShipmentsByStatusRequest { Year = year, Month = month };
            var result = await _getShipmentsByStatusUseCase.ExecuteAsync(ShipmentStatus.Pending, request);
            return Ok(result);
        }

        [HttpGet("in-transit")]
        public async Task<IActionResult> GetInTransitShipments([FromQuery] int year, [FromQuery] int month)
        {
            var request = new GetShipmentsByStatusRequest { Year = year, Month = month };
            var result = await _getShipmentsByStatusUseCase.ExecuteAsync(ShipmentStatus.InTransit, request);
            return Ok(result);
        }

        [HttpGet("delivered")]
        public async Task<IActionResult> GetDeliveredShipments([FromQuery] int year, [FromQuery] int month)
        {
            var request = new GetShipmentsByStatusRequest { Year = year, Month = month };
            var result = await _getShipmentsByStatusUseCase.ExecuteAsync(ShipmentStatus.Delivered, request);
            return Ok(result);
        }

        [HttpGet("delayed")]
        public async Task<IActionResult> GetDelayedShipments([FromQuery] int year, [FromQuery] int month)
        {
            var request = new GetShipmentsByStatusRequest { Year = year, Month = month };
            var result = await _getShipmentsByStatusUseCase.ExecuteAsync(ShipmentStatus.Delayed, request);
            return Ok(result);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentShipments([FromQuery] int limit = 10)
        {
            var result = await _getRecentShipmentsUseCase.ExecuteAsync(limit);
            return Ok(result);
        }
    }

}