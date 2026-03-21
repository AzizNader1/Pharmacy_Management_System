using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetAllUsersQuery : IRequest<List<GetUserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserDto>>
    {
        public async Task<List<GetUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
