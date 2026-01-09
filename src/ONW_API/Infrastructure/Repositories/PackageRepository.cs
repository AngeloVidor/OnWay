using Microsoft.EntityFrameworkCore;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PackageRepository : IPackageRepository
{
    private readonly OnWayDbContext _context;

    public PackageRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Package package)
    {
        await _context.Packages.AddAsync(package);
    }

    public async Task<Package?> GetByIdAsync(Guid packageId)
    {
        return await _context.Packages.FindAsync(packageId);
    }

    public async Task<IEnumerable<Package>> GetByShipmentIdAsync(Guid shipmentId)
    {
        return await _context.Packages
            .Where(p => p.ShipmentId == shipmentId)
            .ToListAsync();
    }

    public async Task<int> GetNextTrackingNumberAsync(int year)
    {
        var count = await _context.Packages
            .CountAsync(p => p.CreatedAt.Year == year);

        return count + 1;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
