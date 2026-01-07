using Microsoft.EntityFrameworkCore;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using OnWay.API.Domain.Entities;
using OnWay.Domain.Transporters;
using OnWay.Domain.Transporters.ValueObjects;

namespace OnWay.Infrastructure.Repositories;

public sealed class TransporterRepository : ITransporterRepository
{
    private readonly OnWayDbContext _context;

    public TransporterRepository(OnWayDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transporter transporter)
    {
        await _context.Transporters.AddAsync(transporter);
    }

    public async Task<Transporter?> GetByIdAsync(Guid id)
    {
        return await _context.Transporters
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Transporter?> GetByEmailAsync(Email email)
    {
        return await _context.Transporters
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<bool> EmailExistsAsync(Email email)
    {
        return await _context.Transporters
            .AnyAsync(x => x.Email.Value == email.Value);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
