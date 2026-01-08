using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.API.DTOs;
using ONW_API.Application.Security;
using ONW_API.Application.Vehicles;

namespace OnWay.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public sealed class VehiclesController : ControllerBase
    {
        private readonly CreateVehicleUseCase _useCase;

        public VehiclesController(CreateVehicleUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("{transporterId}")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto request)
        {
            var transporterId = ClaimsHelper.GetUserId(User);
            try
            {
                var vehicle = await _useCase.ExecuteAsync(request.Plate, request.Model, transporterId);
                return CreatedAtAction(null, new { transporterId, vehicleId = vehicle.Id }, vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
