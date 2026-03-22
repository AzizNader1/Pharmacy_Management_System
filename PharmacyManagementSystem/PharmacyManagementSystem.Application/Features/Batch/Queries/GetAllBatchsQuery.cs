using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Queries
{
    public record GetAllBatchsQuery : IRequest<List<GetBatchDto>>;

    public class GetAllBatchsQueryHandler : IRequestHandler<GetAllBatchsQuery, List<GetBatchDto>>
    {
        private readonly IBatchRepository _batchRepository;

        public GetAllBatchsQueryHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task<List<GetBatchDto>> Handle(GetAllBatchsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
