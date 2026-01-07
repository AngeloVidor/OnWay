using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.Entities;

namespace ONW_API.Domain.Repositories
{
    public interface ITransporterVerificationRepository
    {
        Task AddAsync(TransporterVerification verification);
        Task<TransporterVerification?> GetByCodeAsync(string code);
        Task RemoveAsync(TransporterVerification verification);
        Task SaveChangesAsync();
    }

}