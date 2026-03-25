using MediatR;
using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.Features.Batch.Commands
{
    public record CreateBatchCommand(CreateBatchDto CreateBatchDto) : IRequest<int>;

    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, int>
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IMedicineRepository _medicineRepository;

        public CreateBatchCommandHandler(IBatchRepository batchRepository, IMedicineRepository medicineRepository)
        {
            _batchRepository = batchRepository;
            _medicineRepository = medicineRepository;
        }

        public async Task<int> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateBatchDto == null)
                throw new Exception("you should enter a valid values inside each field");

            var existsBatch = await _batchRepository.GetAllBatchesAsync();
            if (existsBatch.Any(b => b.BatchNumber == request.CreateBatchDto.BatchNumber))
                throw new Exception("this batch number is already exists, please use another batch number");

            var existsMedicine = await _medicineRepository.GetMedicineByIdAsync(request.CreateBatchDto.MedicineId);
            if (existsMedicine == null)
                throw new Exception("there is no medicine exists to the medicine id that you try to use, please add a valid and exists id");

            if (!Enum.IsDefined(typeof(MedicineCategories), request.CreateBatchDto.Category))
                throw new Exception("you should enter a valid category name");

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
