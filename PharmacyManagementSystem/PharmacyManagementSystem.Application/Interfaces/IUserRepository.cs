using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(CreateUserDto createUserDto);
        Task<Guid> DeleteUserAsync(Guid id);
        Task<Guid> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
        Task<GetUserDto> GetUserByIdAsync(Guid id);
        Task<GetUserDto> GetUserByEmailAsync(string email);
        Task<GetUserDto> GetUserByNameAsync(string username);
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
        Task<IEnumerable<GetUserDto>> GetUsersByRoleAsync();


    }
}
