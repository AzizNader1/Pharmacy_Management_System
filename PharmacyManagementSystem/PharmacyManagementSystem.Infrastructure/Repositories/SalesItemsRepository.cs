using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Entities;
using PharmacyManagementSystem.Infrastructure.Data;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class SalesItemsRepository : ISalesItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public SalesItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? CreateSaleItemAsync(SaleItem saleItem)
        {
            await _context.SaleItems.AddAsync(saleItem);
            await _context.SaveChangesAsync();
        }

        public async Task? DeleteSaleItemAsync(int id)
        {
            _context.SaleItems.Remove(_context.SaleItems.Find(id)!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SaleItem?>> GetAllSaleItemesAsync()
        {
            return await _context.SaleItems
                .Include(si => si.Medicine)
                .Include(si => si.Batch)
                .Include(si => si.Sale)
                .ToListAsync();
        }

        public async Task<IEnumerable<SaleItem?>> GetAllSaleItemesBySaleIdAsync(int saleId)
        {
            return await _context.SaleItems
                .Include(si => si.Medicine)
                .Include(si => si.Batch)
                .Where(si => si.SaleId == saleId)
                .ToListAsync();
        }

        public async Task<SaleItem?> GetSaleItemByIdAsync(int id)
        {
            return await _context.SaleItems
                .Include(si => si.Medicine)
                .Include(si => si.Batch)
                .Include(si => si.Sale)
                .FirstOrDefaultAsync(si => si.SaleItemId == id);
        }

        public async Task? UpdateSaleItemAsync(SaleItem saleItem)
        {
            _context.SaleItems.Update(saleItem);
            await _context.SaveChangesAsync();
        }
    }
}
