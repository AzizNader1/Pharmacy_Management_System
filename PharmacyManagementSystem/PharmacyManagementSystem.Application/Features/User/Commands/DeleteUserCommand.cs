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
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value, write a positive value");

            var existUser = await _userRepository.GetUserByIdAsync(request.id);
            if (existUser == null)
                throw new Exception("there is no user exists to this id");

            await _userRepository.DeleteUserAsync(existUser)!;

            return true;
        }
    }
}
