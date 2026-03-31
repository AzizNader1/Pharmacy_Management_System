
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiUserServices
    {
        Task<GetUserDto?> GetUserByIdAsync(int id);
        Task<List<GetUserDto?>> GetAllUsersAsync();
        Task<GetUserDto?> GetUserByEmailAsync(string email);
        Task<GetUserDto?> GetUserByNameAsync(string username);
        Task<List<GetUserDto?>> GetUsersByRoleAsync(string roleName);
    }
}
