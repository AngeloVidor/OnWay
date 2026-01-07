using ONW_API.Domain.Entities;

namespace ONW_API.Domain.Repositories;

public interface IDriverRepository
{
    Task AddAsync(Driver driver);
    Task<Driver?> GetByIdAsync(Guid id);
    Task<IEnumerable<Driver>> GetByTransporterAsync(Guid transporterId);
    Task SaveChangesAsync();
}
