using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record UpdateBatchCommand(int id, UpdateBatchDto updateBatchDto) : IRequest<GetBatchDto>;

    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, GetBatchDto>
    {
        private readonly IBatchRepository _batchRepository;

        public UpdateBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task<GetBatchDto> Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {
            if (request.updateBatchDto == null)
                throw new ArgumentNullException(nameof(request.updateBatchDto), "you should enter a valid data");

            if (request.id == 0)
                throw new ArgumentNullException(nameof(request.id), "you should enter a valid data");

            var existingBatch = await _batchRepository.GetBatchByIdAsync(request.id);
            if (existingBatch == null)
                throw new ArgumentNullException(nameof(existingBatch), "there is no batches avalible to this requested id");


            existingBatch.BatchQuantity = request.updateBatchDto.BatchQuantity;
            existingBatch.ExpiryDate = request.updateBatchDto.ExpiryDate;
            existingBatch.PurchasePrice = request.updateBatchDto.PurchasePrice;

            await _batchRepository.UpdateBatchAsync(existingBatch)!;

            return new GetBatchDto
            {
                BatchId = existingBatch.BatchId,
                BatchQuantity = existingBatch.BatchQuantity,
                ExpiryDate = existingBatch.ExpiryDate,
                PurchasePrice = existingBatch.PurchasePrice,
                BatchNumber = existingBatch.BatchNumber,
                Category = existingBatch.Category,
                MedicineId = existingBatch.MedicineId
            };
        }
    }
}
