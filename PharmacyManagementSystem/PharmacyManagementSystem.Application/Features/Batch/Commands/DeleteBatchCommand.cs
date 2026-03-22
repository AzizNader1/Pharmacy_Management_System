using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record DeleteBatchCommand(int id) : IRequest<bool>;

    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand, bool>
    {
        private readonly IBatchRepository _batchRepository;

        public DeleteBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }
        public Task<bool> Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
