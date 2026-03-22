using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Queries
{
    public record GetBatchDetailsQuery(int id) : IRequest<GetBatchDto>;

    public class GetBatchDetailsQueryHandler : IRequestHandler<GetBatchDetailsQuery, GetBatchDto>
    {
        private readonly IBatchRepository _batchRepository;

        public GetBatchDetailsQueryHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public Task<GetBatchDto> Handle(GetBatchDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
