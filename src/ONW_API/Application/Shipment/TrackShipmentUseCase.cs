using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.API.DTOs;
using ONW_API.Domain.Repositories;

namespace ONW_API.Application.Shipment
{
    public sealed class TrackShipmentUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public TrackShipmentUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<ShipmentTrackingDto> ExecuteAsync(string trackingCode)
        {
            var shipment = await _shipmentRepository.GetByTrackingCodeAsync(trackingCode);
            if (shipment == null)
                throw new InvalidOperationException("Shipment não encontrado.");

            return new ShipmentTrackingDto
            {
                TrackingCode = shipment.TrackingCode,
                Status = shipment.Status.ToString(),
                Origin = $"{shipment.Origin.Address}, {shipment.Origin.City}, {shipment.Origin.State}",
                Destination = $"{shipment.Destination.Address}, {shipment.Destination.City}, {shipment.Destination.State}",
                DriverId = shipment.DriverId,
                Events = shipment.TrackingEvents
                    .OrderBy(e => e.Date)
                    .Select(e => new TrackingEventDto
                    {
                        Date = e.Date,
                        Location = e.Location,
                        Description = e.Description
                    })
                    .ToList()
            };
        }
    }


}