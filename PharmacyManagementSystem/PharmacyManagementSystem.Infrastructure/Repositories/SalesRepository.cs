using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Entities;
using PharmacyManagementSystem.Infrastructure.Data;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _context;

        public SalesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? CreateSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task? DeleteSaleAsync(Sale sale)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale?>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale?>> GetAllSalesByUserIdAsync(int userId)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale?>> GetAllSalesByUserNameAsync(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return await _context.Sales
                .Include(s => s.SaleItems)
                .Where(s => s.UserId == user!.UserId)
                .ToListAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SaleId == id);
        }

        public async Task? UpdateSaleAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}
