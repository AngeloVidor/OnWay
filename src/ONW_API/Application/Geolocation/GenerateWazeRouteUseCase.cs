using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;
using ONW_API.Application.OpenRoute;
using ONW_API.Application.Waze;

namespace ONW_API.Application.Geolocation
{
    public sealed class GenerateWazeRouteUseCase
    {
        private readonly GeocodeSearchUseCase _geocodeSearchUseCase;
        private readonly GenerateWazeLinkUseCase _generateWazeLinkUseCase;

        public GenerateWazeRouteUseCase(
            GeocodeSearchUseCase geocodeSearchUseCase,
            GenerateWazeLinkUseCase generateWazeLinkUseCase)
        {
            _geocodeSearchUseCase = geocodeSearchUseCase;
            _generateWazeLinkUseCase = generateWazeLinkUseCase;
        }

        public async Task<WazeRouteResponse?> ExecuteAsync(WazeRouteRequest request)
        {
            var command = new GeocodeSearchCommand
            {
                Address = new Address(
              request.Street,
              request.Number!,
              request.District,
              request.City,
              request.State,
              request.ZipCode!
          )
            };

            var geoResponse = await _geocodeSearchUseCase.ExecuteAsync(command);

            if (geoResponse?.Features == null || !geoResponse.Features.Any())
                return null;

            var coords = geoResponse.Features[0].Geometry.Coordinates;
            var latitude = coords[1];
            var longitude = coords[0];

            var wazeUrl = _generateWazeLinkUseCase.Execute(latitude, longitude);

            return new WazeRouteResponse(wazeUrl);
        }
    }
}
