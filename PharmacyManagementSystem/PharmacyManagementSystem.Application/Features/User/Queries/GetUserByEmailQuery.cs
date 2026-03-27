using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUserByEmailQuery(string email) : IRequest<GetUserDto>;


    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            if (request.email == null)
                throw new Exception("you should enter a valid email value");

            var user = await _userRepository.GetUserByEmailAsync(request.email);
            if (user == null)
                throw new Exception("there is no user exists for this email");

            return new GetUserDto
            {
                Email = user!.Email,
                PasswordHash = user.PasswordHash,
                FullName = user.FullName,
                UserId = user.UserId,
                UserName = user.UserName,
                UserRole = user.UserRole,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
