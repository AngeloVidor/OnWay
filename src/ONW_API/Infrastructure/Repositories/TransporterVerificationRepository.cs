using Microsoft.EntityFrameworkCore;
using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;

namespace ONW_API.Infrastructure.Repositories;

public sealed class TransporterVerificationRepository
    : ITransporterVerificationRepository
{
    private readonly OnWayDbContext _context;

    public TransporterVerificationRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TransporterVerification verification)
    {
        await _context.TransporterVerifications.AddAsync(verification);
    }

    public async Task<TransporterVerification?> GetByCodeAsync(string code)
    {
        return await _context.TransporterVerifications
            .FirstOrDefaultAsync(v => v.Code == code);
    }

    public Task RemoveAsync(TransporterVerification verification)
    {
        _context.TransporterVerifications.Remove(verification);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
