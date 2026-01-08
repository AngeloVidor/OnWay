using Microsoft.EntityFrameworkCore;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using OnWay.Domain.Transporters.ValueObjects;

namespace OnWay.Infrastructure.Repositories;

public sealed class DriverRepository : IDriverRepository
{
    private readonly OnWayDbContext _context;

    public DriverRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Driver driver)
    {
        await _context.Drivers.AddAsync(driver);
    }

    public void Update(Driver driver)
    {
        _context.Drivers.Update(driver);
    }

    public async Task<Driver?> GetByIdAsync(Guid driverId)
    {
        return await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
    }

    public async Task<IEnumerable<Driver>> GetByTransporterAsync(Guid transporterId)
    {
        return await _context.Drivers
            .Where(d => d.TransporterId == transporterId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
