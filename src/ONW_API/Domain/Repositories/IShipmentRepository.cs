using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Entities;

namespace ONW_API.Domain.Repositories
{
    public interface IShipmentRepository
    {
        Task AddAsync(Shipment shipment);
        Task<Shipment?> GetByIdAsync(Guid id);
        Task<List<Shipment>> GetByTransporterIdAsync(Guid transporterId);
        Task SaveChangesAsync();
        void Update(Shipment shipment);
    }
}