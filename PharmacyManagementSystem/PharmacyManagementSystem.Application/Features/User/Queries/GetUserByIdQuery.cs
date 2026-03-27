using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUserByIdQuery(int id) : IRequest<GetUserDto>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid user id value, write a positive number");

            var user = await _userRepository.GetUserByIdAsync(request.id);
            if (user == null)
                throw new Exception("there is no data exists for this user id in the database");

            return new GetUserDto
            {
                Email = user!.Email,
                PasswordHash = user.PasswordHash,
                FullName = user.FullName,
                UserId = user.UserId,
                UserName = user.UserName,
                UserRole = user.UserRole
            };
        }
    }
}
