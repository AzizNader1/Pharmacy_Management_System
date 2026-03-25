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
        public async Task<bool> Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            if (request.id == 0)
                throw new Exception("the data of the id must be valid data to get the correct data");

            var existsBatch = await _batchRepository.GetBatchByIdAsync(request.id);
            if (existsBatch == null)
                throw new Exception("there is no batches exists to this id");

            await _batchRepository.DeleteBatchAsync(existsBatch)!;
            return true;
        }
    }
}
