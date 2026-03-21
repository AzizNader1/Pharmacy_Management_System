using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record CreateBatchCommand(CreateBatchDto CreateBatchDto) : IRequest<Guid>;

    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, Guid>
    {
        public Task<Guid> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
