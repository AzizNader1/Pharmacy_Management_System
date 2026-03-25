using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Entities;
using PharmacyManagementSystem.Infrastructure.Data;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task? DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Sales)
                .ToListAsync()!;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Sales)
                .FirstOrDefaultAsync(u => u.Email == email)!;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Sales)
                .FirstOrDefaultAsync(u => u.UserId == id)!;
        }

        public async Task<User?> GetUserByNameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Sales)
                .FirstOrDefaultAsync(u => u.UserName == username)!;
        }

        public async Task<IEnumerable<User?>> GetUsersByRoleAsync(string roleName)
        {
            return await _context.Users
                .Include(u => u.Sales)
                .Where(u => u.UserRole.ToString() == roleName)
                .ToListAsync()!;
        }

        public async Task? UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
