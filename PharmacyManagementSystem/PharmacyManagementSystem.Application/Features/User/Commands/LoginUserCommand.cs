using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record LoginUserCommand(LoginRequestDto loginRequestDto) : IRequest<GetUserDto>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByNameAsync(request.loginRequestDto.UserName);
            if (existingUser == null)
            {
                throw new Exception("incorrect username or password");
            }

            if (existingUser.UserName != request.loginRequestDto.UserName)
            {
                throw new Exception("incorrect username or password");
            }

            if (existingUser.Email != request.loginRequestDto.Email)
            {
                throw new Exception("incorrect username or password");
            }

            if (existingUser.PasswordHash != request.loginRequestDto.Password)
            {
                throw new Exception("incorrect username or password");
            }

            return new GetUserDto
            {
                UserId = existingUser.UserId,
                Email = existingUser.Email,
                FullName = existingUser.FullName,
                Password = existingUser.PasswordHash,
                PhoneNumber = existingUser.PhoneNumber,
                UserName = existingUser.UserName,
                UserRole = existingUser.UserRole
            };

        }
    }
}
