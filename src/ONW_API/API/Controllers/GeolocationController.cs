using Microsoft.AspNetCore.Mvc;
using ONW_API.Application.Geolocation;
using ONW_API.Application.OpenRoute;
using ONW_API.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.Controllers
{
    [ApiController]
    [Route("geolocation")]
    public class GeolocationController : ControllerBase
    {
        private readonly GeocodeSearchUseCase _geocodeSearchUseCase;
        private readonly GenerateWazeRouteUseCase _generateWazeRouteUseCase;

        public GeolocationController(
            GeocodeSearchUseCase geocodeSearchUseCase,
        GenerateWazeRouteUseCase generateWazeRouteUseCase)
        {
            _geocodeSearchUseCase = geocodeSearchUseCase;
            _generateWazeRouteUseCase = generateWazeRouteUseCase;
        }

        [HttpPost("geocode")]
        public async Task<IActionResult> GetCoordinates([FromBody] GeocodeSearchCommand command)
        {
            var response = await _geocodeSearchUseCase.ExecuteAsync(command);
            return Ok(response);
        }

        [HttpPost("waze-route")]
        public async Task<IActionResult> GenerateWazeRoute([FromBody] WazeRouteRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");

            var result = await _generateWazeRouteUseCase.ExecuteAsync(request);

            if (result == null)
                return NotFound("Address not found or unable to generate route.");

            return Ok(result);
        }
    }
}
