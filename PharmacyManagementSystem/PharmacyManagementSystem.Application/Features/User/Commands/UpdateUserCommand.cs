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
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value, write a positive number");

            var user = await _userRepository.GetUserByIdAsync(request.id);
            if (user == null)
                throw new Exception("there is no data for this wanted user id");

            user.PhoneNumber = request.UpdateUserDto.PhoneNumber;
            user.FullName = request.UpdateUserDto.FullName;
            user.UserName = request.UpdateUserDto.UserName;

            if (!string.IsNullOrEmpty(request.UpdateUserDto.Password))
            {
                user.PasswordHash = request.UpdateUserDto.Password!;
            }

            await _userRepository.UpdateUserAsync(user)!;

            var returnedUser = new GetUserDto
            {
                UserId = user.UserId,
                UserRole = user.UserRole,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash
            };

            return returnedUser;
        }
    }
}
