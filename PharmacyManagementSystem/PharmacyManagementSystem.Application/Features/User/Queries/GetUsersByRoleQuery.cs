using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUsersByRoleQuery(string role) : IRequest<List<GetUserDto>>;

    public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByRoleQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUserDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            if (request.role == null)
                throw new Exception("you should enter a valid role value");

            var users = await _userRepository.GetUsersByRoleAsync(request.role);
            if (users == null || users.Count() == 0)
                throw new Exception("there is no user exists inside this role at this time");

            var returnedUsers = new List<GetUserDto>();
            foreach (var user in users)
            {
                returnedUsers.Add(new GetUserDto
                {
                    Email = user!.Email,
                    PasswordHash = user.PasswordHash,
                    FullName = user.FullName,
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserRole = user.UserRole,
                    PhoneNumber = user.PhoneNumber
                });
            }

            return returnedUsers;
        }
    }

}
