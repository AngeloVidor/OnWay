using System;
using System.Threading.Tasks;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.Vehicles
{
    public sealed class CreateVehicleUseCase
    {
        private readonly IVehicleRepository _repository;

        public CreateVehicleUseCase(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Vehicle> ExecuteAsync(string plate, string model, Guid transporterId)
        {
            var vehicle = new Vehicle(plate, model, transporterId, VehicleStatus.Available);

            await _repository.AddAsync(vehicle);
            await _repository.SaveChangesAsync();

            return vehicle;
        }
    }
}
