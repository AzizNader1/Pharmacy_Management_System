using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record CreateUserCommand(CreateUserDto CreateUserDto) : IRequest<int>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateUserDto == null) return 0!;

            var user = new Domain.Entities.User
            {
                Email = request.CreateUserDto.Email,
                FullName = request.CreateUserDto.FullName,
                PhoneNumber = request.CreateUserDto.PhoneNumber,
                UserName = request.CreateUserDto.UserName,
                UserRole = request.CreateUserDto.UserRole,
                PasswordHash = request.CreateUserDto.Password
            };

            await _userRepository.CreateUserAsync(user)!;
            return user.UserId;
        }
    }
}
