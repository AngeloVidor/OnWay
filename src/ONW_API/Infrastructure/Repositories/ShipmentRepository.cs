// using Microsoft.EntityFrameworkCore;
// using OnWay.API.Domain.Entities;
// using ONW_API.Domain.Repositories;
// using ONW_API.Infrastructure.Data;
// using ONW_API.Domain.Entities;
// using ONW_API.Domain.ValueObjects;

// namespace OnWay.Infrastructure.Repositories;

// public sealed class ShipmentRepository : IShipmentRepository
// {
//     private readonly OnWayDbContext _context;

//     public ShipmentRepository(OnWayDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<Shipment?> GetByIdAsync(Guid id)
//     {
//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Include(s => s.TrackingEvents)
//             .FirstOrDefaultAsync(s => s.Id == id);
//     }

//     public async Task<List<Shipment>> GetByTransporterIdAsync(Guid transporterId)
//     {
//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Where(s => s.TransporterId == transporterId)
//             .ToListAsync();
//     }

//     public async Task AddAsync(Shipment shipment)
//     {
//         await _context.Shipments.AddAsync(shipment);
//     }

//     public void Update(Shipment shipment)
//     {
//         _context.Shipments.Update(shipment);
//     }

//     public async Task SaveChangesAsync()
//     {
//         await _context.SaveChangesAsync();
//     }

//     public async Task<List<Shipment>> GetShipmentsByStatusAndMonthAsync(ShipmentStatus status, Guid transporterId, int year, int month)
//     {
//         var startDate = new DateTime(year, month, 1);
//         var endDate = startDate.AddMonths(1);

//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Include(s => s.Origin)
//             .Include(s => s.Destination)
//             .Where(s =>
//                 s.TransporterId == transporterId &&
//                 s.Status == status &&
//                 s.CreatedAt >= startDate &&
//                 s.CreatedAt < endDate
//             )
//             .ToListAsync();
//     }


//     public async Task<List<Shipment>> GetRecentShipmentsAsync(Guid transporterId, int limit)
//     {
//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Include(s => s.Origin)
//             .Include(s => s.Destination)
//             .Include(s => s.TrackingEvents)
//             .Where(s => s.TransporterId == transporterId)
//             .OrderByDescending(s => s.CreatedAt)
//             .Take(limit)
//             .ToListAsync();
//     }


//     public async Task<List<Shipment>> GetActiveShipmentsAsync(Guid transporterId, int year, int month)
//     {
//         var startDate = new DateTime(year, month, 1);
//         var endDate = startDate.AddMonths(1);

//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Include(s => s.Origin)
//             .Include(s => s.Destination)
//             .Where(s =>
//                 s.TransporterId == transporterId &&
//                 (s.Status == ShipmentStatus.Pending || s.Status == ShipmentStatus.InTransit) &&
//                 s.CreatedAt >= startDate &&
//                 s.CreatedAt < endDate
//             )
//             .ToListAsync();
//     }

//     public async Task<int> GetNextTrackingNumberAsync(int year)
//     {
//         var count = await _context.Shipments
//             .CountAsync(s => s.CreatedAt.Year == year);
//         return count + 1;
//     }

//     public async Task<Shipment?> GetByTrackingCodeAsync(string trackingCode)
//     {
//         return await _context.Shipments
//             .Include(s => s.Products)
//             .Include(s => s.TrackingEvents)
//             .FirstOrDefaultAsync(s => s.TrackingCode == trackingCode);
//     }


// }


