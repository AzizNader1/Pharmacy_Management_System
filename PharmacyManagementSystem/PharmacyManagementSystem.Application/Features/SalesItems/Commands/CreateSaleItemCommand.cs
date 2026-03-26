using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record CreateSaleItemCommand(CreateSaleItemDto createSaleItemDto) : IRequest<int>;

    public class CreateSaleItemCommandHandler : IRequestHandler<CreateSaleItemCommand, int>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly ISalesRepository _salesRepository;

        public CreateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository, IMedicineRepository medicineRepository, IBatchRepository batchRepository, ISalesRepository salesRepository)
        {
            _salesItemsRepository = salesItemsRepository;
            _medicineRepository = medicineRepository;
            _batchRepository = batchRepository;
            _salesRepository = salesRepository;
        }

        public async Task<int> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
        {
            if (request.createSaleItemDto == null)
                throw new Exception("you should enter a valid values to each field");

            var existsSale = await _salesRepository.GetSaleByIdAsync(request.createSaleItemDto.SaleId);
            if (existsSale == null)
                throw new Exception("there is no sale exists for this sale id that you are trying to use, please add sale record first to be able to add items inside it");

            var existsBatch = await _batchRepository.GetBatchByIdAsync(request.createSaleItemDto.BatchId);
            if (existsBatch == null)
                throw new Exception("there is no related batch to this batch id that you are trying to include, please ensure that you are using correct batch id or add a new one to be able to make this process complete");

            var existsMedicine = await _medicineRepository.GetMedicineByIdAsync(request.createSaleItemDto.MedicineId);
            if (existsMedicine == null)
                throw new Exception("there is no medicine exists in the database for this medicine id that you are trying to add to your sale");

            if (request.createSaleItemDto.ItemQuantity > existsMedicine.TotalStock)
                throw new Exception("there is no avalible quantity inside the stock for this medicine that you try to sale");

            var saleItem = new Domain.Entities.SaleItem
            {
                SaleId = request.createSaleItemDto.SaleId,
                UnitPrice = request.createSaleItemDto.UnitPrice,
                ItemQuantity = request.createSaleItemDto.ItemQuantity,
                BatchId = request.createSaleItemDto.BatchId,
                MedicineId = request.createSaleItemDto.MedicineId
            };

            await _salesItemsRepository.CreateSaleItemAsync(saleItem)!;
            return saleItem.SaleItemId;
        }
    }
}
