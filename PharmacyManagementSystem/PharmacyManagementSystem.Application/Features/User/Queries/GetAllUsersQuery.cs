using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetAllUsersQuery : IRequest<List<GetUserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var returnedUsers = new List<GetUserDto>();

            var users = await _userRepository.GetAllUsersAsync();
            if (users.Count() == 0 || users == null)
                throw new Exception("there is no user exists at this time in the database");

            foreach (var user in users)
            {
                returnedUsers.Add(new GetUserDto
                {
                    UserName = user!.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    Password = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    UserId = user.UserId,
                    UserRole = user.UserRole
                });
            }
            return returnedUsers;
        }
    }
}
