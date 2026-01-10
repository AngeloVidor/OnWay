using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Deliveries;
using ONW_API.Application.Shipments;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly StartRouteUseCase _startRouteUseCase;
        private readonly GetShipmentPackagesUseCase _getShipmentPackagesUseCase;


        public DeliveriesController(StartRouteUseCase startRouteUseCase, GetShipmentPackagesUseCase getShipmentPackagesUseCase)
        {
            _startRouteUseCase = startRouteUseCase;
            _getShipmentPackagesUseCase = getShipmentPackagesUseCase;
        }

        [HttpPost("start-route")]
        public async Task<IActionResult> StartRoute(Guid driverId)
        {
            if (driverId == Guid.Empty)
                return BadRequest("DriverId is required.");

            try
            {
                var result = await _startRouteUseCase.ExecuteAsync(driverId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("shipments/{shipmentId}/packages")]
        public async Task<IActionResult> GetShipmentPackages(Guid shipmentId)
        {
            if (shipmentId == Guid.Empty)
                return BadRequest("ShipmentId is required.");

            var packages = await _getShipmentPackagesUseCase.ExecuteAsync(shipmentId);

            if (packages == null || packages.Count == 0)
                return NotFound("No packages found for this shipment.");

            return Ok(packages);
        }

    }
}
