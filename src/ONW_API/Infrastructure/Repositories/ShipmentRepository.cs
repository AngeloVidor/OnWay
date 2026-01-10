using Microsoft.EntityFrameworkCore;
using OnWay.API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using ONW_API.Domain.Entities;
using ONW_API.Domain.ValueObjects;
using ONW_API.Application.Deliveries;
using ONW_API.Infrastructure.Responses;

namespace OnWay.Infrastructure.Repositories;

public sealed class ShipmentRepository : IShipmentRepository
{
    private readonly OnWayDbContext _context;

    public ShipmentRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
    }

    public void Update(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        return await _context.Shipments.FindAsync(id);
    }

    public async Task<ShipmentDriverVehicleDto?> GetShipmentWithVehicleByDriverAsync(Guid driverId)
    {
        return await _context.Shipments
            .Where(s => s.DriverId == driverId)
            .Select(s => new ShipmentDriverVehicleDto
            {
                ShipmentId = s.Id,
                TrackingCode = s.TrackingCode,
                ShipmentStatus = s.Status,
                DriverId = s.DriverId.Value,
                VehicleId = s.VehicleId
            })
            .FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Shipment>> GetShipmentsByStatusAndMonthAsync(ShipmentStatus status, Guid transporterId, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1);

        return await _context.Shipments
            .Include(s => s.Packages)
            .Where(s =>
                s.TransporterId == transporterId &&
                s.Status == status &&
                s.CreatedAt >= startDate &&
                s.CreatedAt < endDate
            )
            .ToListAsync();
    }

    public async Task<List<Shipment>> GetRecentShipmentsAsync(Guid transporterId, int limit)
    {
        return await _context.Shipments
            .Include(s => s.Packages)
            .Where(s => s.TransporterId == transporterId)
            .OrderByDescending(s => s.CreatedAt)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<Shipment>> GetActiveShipmentsAsync(Guid transporterId, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1);

        return await _context.Shipments
            .Include(s => s.Packages)
            .Where(s =>
                s.TransporterId == transporterId &&
                (s.Status == ShipmentStatus.Created ||
                 s.Status == ShipmentStatus.InTransit) &&
                s.CreatedAt >= startDate &&
                s.CreatedAt < endDate
            )
            .ToListAsync();
    }

    public async Task<int> GetNextTrackingNumberAsync(int year)
    {
        var count = await _context.Shipments
            .CountAsync(s => s.CreatedAt.Year == year);
        return count + 1;
    }

    public async Task<Shipment?> GetByTrackingCodeAsync(string trackingCode)
    {
        return await _context.Shipments
            .Include(s => s.Packages)
            .FirstOrDefaultAsync(s => s.TrackingCode == trackingCode);
    }

    public async Task<List<ShipmentDetailsResponse>> GetShipmentDetailsAsync(Guid shipmentId, Guid transporterId)
    {
        return await _context.Shipments
            .AsNoTracking()
            .Where(s => s.Id == shipmentId && s.TransporterId == transporterId)
            .Select(s => new ShipmentDetailsResponse
            {
                id = s.Id,
                transporter_id = s.TransporterId,
                vehicle_id = s.VehicleId,
                driver_id = s.DriverId,
                tracking_code = s.TrackingCode,
                status = s.Status,
                created_at = s.CreatedAt,

                origin = s.Origin,
                destination = s.Destination,

                packages = s.Packages.Select(p => new PackageResponse
                {
                    id = p.Id,
                    tracking_code = p.TrackingCode,
                    status = p.Status,
                    created_at = p.CreatedAt,
                    recipient = p.Recipient
                }).ToList()
            })
            .ToListAsync();
    }
}





