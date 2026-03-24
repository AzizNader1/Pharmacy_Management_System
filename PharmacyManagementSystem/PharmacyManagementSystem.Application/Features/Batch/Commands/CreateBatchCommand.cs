using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record CreateBatchCommand(CreateBatchDto CreateBatchDto) : IRequest<int>;

    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, int>
    {
        private readonly IBatchRepository _batchRepository;

        public CreateBatchCommandHandler(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task<int> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateBatchDto == null)
                throw new ArgumentNullException(nameof(request.CreateBatchDto), "You should enter a valid data");

            var batch = new Domain.Entities.Batch()
            {
                BatchNumber = request.CreateBatchDto.BatchNumber,
                BatchQuantity = request.CreateBatchDto.BatchQuantity,
                Category = request.CreateBatchDto.Category,
                ExpiryDate = request.CreateBatchDto.ExpiryDate,
                PurchasePrice = request.CreateBatchDto.PurchasePrice,
                MedicineId = request.CreateBatchDto.MedicineId,
            };

            await _batchRepository.CreateBatchAsync(batch)!;

            return batch.BatchId;
        }
    }
}
