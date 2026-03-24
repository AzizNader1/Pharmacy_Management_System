using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUserByNameQuery(string name) : IRequest<GetUserDto>;

    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.name == null)
                throw new Exception("you should enter a valid user name");

            var user = await _userRepository.GetUserByNameAsync(request.name);
            if (user == null)
                throw new Exception("there is no data exists for this name in the database");

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
