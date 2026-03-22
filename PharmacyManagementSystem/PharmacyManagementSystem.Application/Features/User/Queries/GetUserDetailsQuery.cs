using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUserDetailsQuery(int id) : IRequest<GetUserDto>;

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserDetailsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.id == 0) return null!;

            var user = await _userRepository.GetUserByIdAsync(request.id);
            if (user == null) return null!;

            return new GetUserDto
            {
                Email = user!.Email,
                Password = user.PasswordHash,
                FullName = user.FullName,
                UserId = user.UserId,
                UserName = user.UserName,
                UserRole = user.UserRole
            };
        }
    }
}
