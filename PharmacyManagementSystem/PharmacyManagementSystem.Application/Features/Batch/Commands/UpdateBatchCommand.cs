using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record UpdateBatchCommand(int id, UpdateBatchDto UpdateBatchDto) : IRequest<GetBatchDto>;

    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, GetBatchDto>
    {
        private readonly IBatchRepository _batchRepository;

        public UpdateBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public Task<GetBatchDto> Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
