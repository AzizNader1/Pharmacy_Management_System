using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record CreateBatchCommand(CreateBatchDto CreateBatchDto) : IRequest<int>;

    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, int>
    {
        private readonly IBatchRepository _batchRepository;

        public CreateBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public Task<int> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
