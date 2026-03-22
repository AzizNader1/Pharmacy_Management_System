using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record UpdateUserCommand(int id, UpdateUserDto UpdateUserDto) : IRequest<GetUserDto>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.id == 0) return null!;

            var user = await _userRepository.GetUserByIdAsync(request.id);
            if (user == null) return null!;

            user.PhoneNumber = request.UpdateUserDto.PhoneNumber;
            user.FullName = request.UpdateUserDto.FullName;
            user.UserName = request.UpdateUserDto.UserName;
            user.PasswordHash = request.UpdateUserDto.Password;

            await _userRepository.UpdateUserAsync(user)!;

            var newUser = await _userRepository.GetUserByIdAsync(request.id);
            if (newUser == null) return null!;

            var returnedUser = new GetUserDto
            {
                Email = newUser.Email,
                FullName = newUser.FullName,
                UserName = newUser.UserName,
                PhoneNumber = newUser.PhoneNumber,
                Password = newUser.PasswordHash
            };

            return returnedUser;
        }
    }
}
