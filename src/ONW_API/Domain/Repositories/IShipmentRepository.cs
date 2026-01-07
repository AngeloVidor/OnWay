using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Entities;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Domain.Repositories
{
    public interface IShipmentRepository
    {
        Task AddAsync(Shipment shipment);
        Task<Shipment?> GetByIdAsync(Guid id);
        Task<List<Shipment>> GetByTransporterIdAsync(Guid transporterId);
        Task SaveChangesAsync();
        void Update(Shipment shipment);
        Task<List<Shipment>> GetShipmentsByStatusAndMonthAsync(ShipmentStatus status, int year, int month);
        Task<List<Shipment>> GetActiveShipmentsAsync(int year, int month);
        Task<int> GetNextTrackingNumberAsync(int year);
        Task<List<Shipment>> GetRecentShipmentsAsync(int limit);
        Task<Shipment?> GetByTrackingCodeAsync(string trackingCode);
    }
}