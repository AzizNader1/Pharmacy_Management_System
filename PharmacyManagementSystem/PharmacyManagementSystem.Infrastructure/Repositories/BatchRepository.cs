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

        public async Task? DeleteBatchAsync(int id)
        {
            _context.Batches.Remove(_context.Batches.Find(id)!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Batch?>> GetAllBatchesAsync()
        {
            return await _context.Batches.ToListAsync()!;
        }

        public async Task<Batch?> GetBatchByIdAsync(int id)
        {
            return await _context.Batches.FirstOrDefaultAsync(b => b.BatchId == id)!;
        }

        public async Task? UpdateBatchAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await _context.SaveChangesAsync();
        }
    }
}
