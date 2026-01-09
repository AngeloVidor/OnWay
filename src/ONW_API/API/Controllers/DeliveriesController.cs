using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Deliveries;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly StartRouteUseCase _startRouteUseCase;

        public DeliveriesController(StartRouteUseCase startRouteUseCase)
        {
            _startRouteUseCase = startRouteUseCase;
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
    }
}
