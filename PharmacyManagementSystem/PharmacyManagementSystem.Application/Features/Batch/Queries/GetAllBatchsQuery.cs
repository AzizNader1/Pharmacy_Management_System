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
            var batches = await _batchRepository.GetAllBatchesAsync();
            if (batches.Count() == 0 || batches == null)
                throw new Exception("there is no batches exists in the database right now, please try again later");

            var returnedBatches = new List<GetBatchDto>();
            foreach (var batch in batches)
            {
                returnedBatches.Add(new GetBatchDto
                {
                    BatchId = batch!.BatchId,
                    BatchNumber = batch!.BatchNumber,
                    BatchQuantity = batch!.BatchQuantity,
                    Category = batch!.Category,
                    ExpiryDate = batch!.ExpiryDate,
                    MedicineId = batch!.MedicineId,
                    PurchasePrice = batch!.PurchasePrice
                });
            }

            return returnedBatches;
        }
    }
}
