using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnWay.API.Domain.Entities;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.Repositories
{
    public interface ITransporterRepository
    {
        Task AddAsync(Transporter transporter);
        Task<Transporter?> GetByIdAsync(Guid id);
        Task<Transporter?> GetByEmailAsync(Email email);
        Task<bool> EmailExistsAsync(Email email);
        Task SaveChangesAsync();
    }
}