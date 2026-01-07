using Microsoft.EntityFrameworkCore;
using OnWay.API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using ONW_API.Domain.Entities;

namespace OnWay.Infrastructure.Repositories;

public sealed class ShipmentRepository : IShipmentRepository
{
    private readonly OnWayDbContext _context;

    public ShipmentRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        return await _context.Shipments
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Shipment>> GetByTransporterIdAsync(Guid transporterId)
    {
        return await _context.Shipments
            .Include(s => s.Products)
            .Where(s => s.TransporterId == transporterId)
            .ToListAsync();
    }

    public async Task AddAsync(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
    }

    public void Update(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
