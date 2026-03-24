using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record UpdateSaleItemCommand(int id, UpdateSaleItemDto updateSaleItemDto) : IRequest<GetSaleItemDto>;

    public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand, GetSaleItemDto>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public UpdateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<GetSaleItemDto> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0 || request.updateSaleItemDto == null)
                throw new Exception("you should enter a valid data to make the process goes well");

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
