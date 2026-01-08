using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;

namespace OnWay.Infrastructure.Repositories
{
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly OnWayDbContext _context;

        public VehicleRepository(OnWayDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task<bool> ExistsAsync(Guid vehicleId)
        {
            return await _context.Vehicles.AnyAsync(v => v.Id == vehicleId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
