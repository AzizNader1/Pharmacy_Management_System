using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiAccountServices
    {
        Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<AuthResponseDto> Register(CreateUserDto createUserDto);
        Task<bool> Logout();
    }
}
