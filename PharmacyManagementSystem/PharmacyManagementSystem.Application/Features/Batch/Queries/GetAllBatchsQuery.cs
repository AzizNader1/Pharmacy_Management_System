using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.Application.Features.Batch.Queries
{
    public record GetAllBatchsQuery : IRequest<List<GetBatchDto>>;

    public class GetAllBatchsQueryHandler : IRequestHandler<GetAllBatchsQuery, List<GetBatchDto>>
    {
        public async Task<List<GetBatchDto>> Handle(GetAllBatchsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
