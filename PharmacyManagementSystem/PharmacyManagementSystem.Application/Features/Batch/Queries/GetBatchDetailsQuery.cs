using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.Application.Features.Batch.Queries
{
    public record GetBatchDetailsQuery(Guid id) : IRequest<GetBatchDto>;

    public class GetBatchDetailsQueryHandler : IRequestHandler<GetBatchDetailsQuery, GetBatchDto>
    {
        public Task<GetBatchDto> Handle(GetBatchDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
