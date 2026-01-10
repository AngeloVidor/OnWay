using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Application.Deliveries;
using ONW_API.Domain.Entities;
using ONW_API.Domain.ValueObjects;
using ONW_API.Infrastructure.Responses;

namespace ONW_API.Domain.Repositories
{
    public interface IShipmentRepository
    {
        Task AddAsync(Shipment shipment);
        Task<Shipment?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
        void Update(Shipment shipment);
        Task<List<Shipment>> GetShipmentsByStatusAndMonthAsync(ShipmentStatus status, Guid transporterId, int year, int month);
        Task<List<Shipment>> GetActiveShipmentsAsync(Guid transporterId, int year, int month);
        Task<int> GetNextTrackingNumberAsync(int year);
        Task<List<Shipment>> GetRecentShipmentsAsync(Guid transporterId, int limit);
        Task<Shipment?> GetByTrackingCodeAsync(string trackingCode);
        Task<ShipmentDriverVehicleDto?> GetShipmentWithVehicleByDriverAsync(Guid driverId);
        Task<List<ShipmentDetailsResponse>> GetShipmentDetailsAsync(Guid shipmentId, Guid transporterId);
    }
}