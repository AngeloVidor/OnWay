using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ONW_API.Application.OpenRoute;
using ONW_API.Domain.ValueObjects;
using ONW_API.Infrastructure.OpenRoute;

namespace ONW_API.Application.Geolocation
{
    public sealed class GeocodeSearchUseCase
    {
        private readonly HttpClient _httpClient;
        private readonly OpenRouteSettings _openRoute;

        public GeocodeSearchUseCase(HttpClient httpClient, OpenRouteSettings openRoute)
        {
            _httpClient = httpClient;
            _openRoute = openRoute;
        }

        public async Task<GeocodeResponse?> ExecuteAsync(GeocodeSearchCommand command)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "api_key", _openRoute.ApiKey },
                { "country", "BR" }
            };

            if (!string.IsNullOrWhiteSpace(command.Address.Street))
                queryParams["address"] = command.Address.Street;

            if (!string.IsNullOrWhiteSpace(command.Address.Number))
                queryParams["housenumber"] = command.Address.Number;

            if (!string.IsNullOrWhiteSpace(command.Address.City))
                queryParams["locality"] = command.Address.City;

            if (!string.IsNullOrWhiteSpace(command.Address.State))
                queryParams["region"] = command.Address.State;

            if (!string.IsNullOrWhiteSpace(command.Address.ZipCode))
                queryParams["postalcode"] = command.Address.ZipCode;

            var queryString = string.Join("&",
                queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            var url = $"https://api.openrouteservice.org/geocode/search/structured?{queryString}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<GeocodeResponse>();
        }
    }


}
