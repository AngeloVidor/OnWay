using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Security;
using ONW_API.Application.Shipment;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public sealed class ShipmentsController : ControllerBase
    {
        private readonly CreateShipmentUseCase _useCase;

        public ShipmentsController(CreateShipmentUseCase useCase)
        {
            _useCase = useCase;
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
    }

}