using System;
using System.Threading.Tasks;
using ONW_API.Domain.Entities;

namespace ONW_API.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
        Task<bool> ExistsAsync(Guid vehicleId);
        Task SaveChangesAsync();
    }
}
