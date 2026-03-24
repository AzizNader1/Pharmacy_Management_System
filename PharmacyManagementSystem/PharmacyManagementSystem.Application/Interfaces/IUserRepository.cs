using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        Task? CreateUserAsync(User user);
        Task? DeleteUserAsync(int id);
        Task? UpdateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User?>> GetAllUsersAsync();
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByNameAsync(string username);
        Task<IEnumerable<User?>> GetUsersByRoleAsync(string roleName);

    }
}
