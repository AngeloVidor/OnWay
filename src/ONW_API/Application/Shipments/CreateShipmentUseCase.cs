// CreateShipmentUseCase.cs
using System.Runtime.Intrinsics.Arm;
using ONW_API.API.DTOs;
using ONW_API.Application.Services;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.UseCases
{
    public sealed class CreateShipmentUseCase
    {
        private readonly IShipmentRepository _repository;
        private readonly ITrackingNumberGenerator _trackingGenerator;


        public CreateShipmentUseCase(IShipmentRepository repository, ITrackingNumberGenerator trackingGenerator)
        {
            _repository = repository;
            _trackingGenerator = trackingGenerator;
        }

        public async Task<Domain.Entities.Shipment> ExecuteAsync(Guid transporterId, LocationDto originDto, LocationDto destinationDto)
        {
            var origin = new Location(
                originDto.Address,
                originDto.City,
                originDto.State
            );

            var destination = new Location(
                destinationDto.Address,
                destinationDto.City,
                destinationDto.State
            );

            var year = DateTime.UtcNow.Year;
            var nextTrackingNumber = await _repository.GetNextTrackingNumberAsync(year);

            var shipment = new Domain.Entities.Shipment(
                transporterId,
                origin,
                destination,
                () => nextTrackingNumber
            );

            await _repository.AddAsync(shipment);
            await _repository.SaveChangesAsync();

            return shipment;
        }
    }

}
