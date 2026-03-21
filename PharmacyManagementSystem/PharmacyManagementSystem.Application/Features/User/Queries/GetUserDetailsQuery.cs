using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.Application.Features.User.Queries
{
    public record GetUserDetailsQuery(Guid id) : IRequest<GetUserDto>;

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDto>
    {
        public Task<GetUserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
