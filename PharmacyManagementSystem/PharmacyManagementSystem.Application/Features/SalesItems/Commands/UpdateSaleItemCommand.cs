using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record UpdateSaleItemCommand(int id, UpdateSaleItemDto updateSaleItemDto) : IRequest<GetSaleItemDto>;

    public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand, GetSaleItemDto>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IMedicineRepository _medicineRepository;

        public UpdateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository, ISalesRepository salesRepository, IBatchRepository batchRepository, IMedicineRepository medicineRepository)
        {
            _salesItemsRepository = salesItemsRepository;
            _salesRepository = salesRepository;
            _batchRepository = batchRepository;
            _medicineRepository = medicineRepository;
        }

        public async Task<GetSaleItemDto> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0 || request.updateSaleItemDto == null)
                throw new Exception("you should enter a valid data to make the process goes well");

            var existsSale = await _salesRepository.GetSaleByIdAsync(request.updateSaleItemDto.SaleId);
            if (existsSale == null)
                throw new Exception("there is no sale exists for this sale id that you are trying to use, please add sale record first to be able to add items inside it");

            var existsBatch = await _batchRepository.GetBatchByIdAsync(request.updateSaleItemDto.BatchId);
            if (existsBatch == null)
                throw new Exception("there is no related batch to this batch id that you are trying to include, please ensure that you are using correct batch id or add a new one to be able to make this process complete");

            var existsMedicine = await _medicineRepository.GetMedicineByIdAsync(request.updateSaleItemDto.MedicineId);
            if (existsMedicine == null)
                throw new Exception("there is no medicine exists in the database for this medicine id that you are trying to add to your sale");

            if (request.updateSaleItemDto.ItemQuantity > existsMedicine.TotalStock)
                throw new Exception("there is no avalible quantity inside the stock for this medicine that you try to sale");

            var existingSaleItem = await _salesItemsRepository.GetSaleItemByIdAsync(request.id);
            if (existingSaleItem == null)
                throw new Exception("there is no data exists for this requested id");

            existingSaleItem.UnitPrice = request.updateSaleItemDto.UnitPrice;
            existingSaleItem.ItemQuantity = request.updateSaleItemDto.ItemQuantity;

            await _salesItemsRepository.UpdateSaleItemAsync(existingSaleItem)!;

            var newSaleItemData = new GetSaleItemDto
            {
                BatchId = existingSaleItem.BatchId,
                SaleItemId = existingSaleItem.SaleItemId,
                ItemQuantity = existingSaleItem.ItemQuantity,
                MedicineId = existingSaleItem.MedicineId,
                SaleId = existingSaleItem.SaleId,
                UnitPrice = existingSaleItem.UnitPrice
            };
            return newSaleItemData;

        }
    }
}
