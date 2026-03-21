using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<Guid> CreateUserAsync(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserDto>> GetUsersByRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
