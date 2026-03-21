using MediatR;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record UpdateUserCommand(Guid id, UpdateUserDto UpdateUserDto) : IRequest<GetUserDto>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
    {
        public Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
