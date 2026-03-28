
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiUserServices
    {
        Task<GetUserDto?> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserDto?>> GetAllUsersAsync();
        Task<GetUserDto?> GetUserByEmailAsync(string email);
        Task<GetUserDto?> GetUserByNameAsync(string username);
        Task<IEnumerable<GetUserDto?>> GetUsersByRoleAsync(string roleName);
    }
}
