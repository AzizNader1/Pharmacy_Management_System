using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record UpdateBatchCommand(Guid id, UpdateBatchDto UpdateBatchDto) : IRequest<GetBatchDto>;

    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, GetBatchDto>
    {
        public Task<GetBatchDto> Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
