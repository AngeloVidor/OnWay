using ONW_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONW_API.Domain.Repositories
{
    public interface IPackageRepository
    {
        Task AddAsync(Package package);
        Task<Package?> GetByIdAsync(Guid packageId);
        Task<IEnumerable<Package>> GetByShipmentIdAsync(Guid shipmentId);
        Task<int> GetNextTrackingNumberAsync(int year);
        Task SaveChangesAsync();
    }
}
