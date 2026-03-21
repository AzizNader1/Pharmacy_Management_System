using MediatR;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record DeleteUserCommand(Guid id) : IRequest<bool>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
