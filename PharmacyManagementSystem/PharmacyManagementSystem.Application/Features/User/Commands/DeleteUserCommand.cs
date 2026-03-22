using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record DeleteUserCommand(int id) : IRequest<bool>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.id == 0) return false;

            await _userRepository.DeleteUserAsync(request.id)!;

            return true;
        }
    }
}
