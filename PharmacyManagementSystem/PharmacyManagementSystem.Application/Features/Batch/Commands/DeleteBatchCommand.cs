using MediatR;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record DeleteBatchCommand(Guid id) : IRequest<bool>;

    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand, bool>
    {
        public Task<bool> Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
