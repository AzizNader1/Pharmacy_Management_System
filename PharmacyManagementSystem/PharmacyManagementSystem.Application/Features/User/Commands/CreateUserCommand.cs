using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record CreateUserCommand(CreateUserDto createUserDto) : IRequest<int>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByNameAsync(request.createUserDto.UserName);
            if (existingUser != null)
            {
                throw new Exception($"A user with this name : {request.createUserDto.UserName} already exists.");
            }

            var existingEmail = await _userRepository.GetUserByEmailAsync(request.createUserDto.Email);
            if (existingEmail != null)
            {
                throw new Exception($"A user with this email : {request.createUserDto.Email} already exists.");
            }

            var user = new Domain.Entities.User
            {
                Email = request.createUserDto.Email,
                FullName = request.createUserDto.FullName,
                PhoneNumber = request.createUserDto.PhoneNumber,
                UserName = request.createUserDto.UserName,
                UserRole = request.createUserDto.UserName.Contains("admin") ? UserRoles.Admin : UserRoles.Cashier,
                PasswordHash = request.createUserDto.Password
            };

            await _userRepository.CreateUserAsync(user)!;
            return user.UserId;
        }
    }
}
