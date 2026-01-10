using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using ONW_API.Application.OpenRoute;
using ONW_API.Domain.ValueObjects;
using ONW_API.Infrastructure.Geoapify;

namespace ONW_API.Application.Geolocation
{
    public sealed class GeocodeSearchUseCase
    {
        private readonly HttpClient _httpClient;
        private readonly GeoapifySettings _geoapify;

        public GeocodeSearchUseCase(HttpClient httpClient, GeoapifySettings geoapify)
        {
            _httpClient = httpClient;
            _geoapify = geoapify;
        }

        public async Task<Result?> ExecuteAsync(GeocodeSearchCommand command)
        {
            var addressText = $"{command.Address.Number} {command.Address.Street}, {command.Address.City} {command.Address.ZipCode}, {command.Address.State}";
            var encodedAddress = HttpUtility.UrlEncode(addressText);

            var url = $"https://api.geoapify.com/v1/geocode/search?text={encodedAddress}&format=json&apiKey={_geoapify.ApiKey}";

            var response = await _httpClient.GetFromJsonAsync<GeocodeResponseDto>(url);

            if (response?.Results == null || !response.Results.Any())
                return null; 
                
            var firstResult = response.Results.First();

            return new Result
            {
                lon = firstResult.Lon,
                lat = firstResult.Lat
            };
        }
    }


}
