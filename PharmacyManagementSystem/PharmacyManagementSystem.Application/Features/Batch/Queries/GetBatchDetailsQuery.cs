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

        public async Task<GetBatchDto> Handle(GetBatchDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("this id is not a valid one, please enter a positive valid id and try again");

            var batch = await _batchRepository.GetBatchByIdAsync(request.id);
            if (batch == null)
                throw new Exception("there is no batches avalible to this request id");

            var returnedBatch = new GetBatchDto
            {
                BatchId = batch.BatchId,
                BatchNumber = batch.BatchNumber,
                BatchQuantity = batch.BatchQuantity,
                Category = batch.Category,
                ExpiryDate = batch.ExpiryDate,
                MedicineId = batch.MedicineId,
                PurchasePrice = batch.PurchasePrice
            };

            return returnedBatch;
        }
    }
}
