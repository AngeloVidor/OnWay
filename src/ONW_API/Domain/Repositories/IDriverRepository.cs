using ONW_API.Domain.Entities;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.Repositories;

public interface IDriverRepository
{
    Task AddAsync(Driver driver);
    Task<Driver?> GetByIdAsync(Guid driverId);
    Task<IEnumerable<Driver>> GetByTransporterAsync(Guid transporterId);
    Task SaveChangesAsync();
    void Update(Driver driver);

}
