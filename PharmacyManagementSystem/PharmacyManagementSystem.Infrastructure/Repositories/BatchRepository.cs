using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Entities;
using PharmacyManagementSystem.Infrastructure.Data;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly ApplicationDbContext _context;

        public BatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? CreateBatchAsync(Batch batch)
        {
            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
        }

        public async Task? DeleteBatchAsync(Batch batch)
        {
            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Batch?>> GetAllBatchesAsync()
        {
            return await _context.Batches
                .Include(b => b.SaleItems)
                .Include(b => b.Medicine)
                .ToListAsync()!;
        }

        public async Task<Batch?> GetBatchByIdAsync(int id)
        {
            return await _context.Batches
                .Include(b => b.SaleItems)
                .Include(b => b.Medicine)
                .FirstOrDefaultAsync(b => b.BatchId == id)!;
        }

        public async Task? UpdateBatchAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await _context.SaveChangesAsync();
        }
    }
}
