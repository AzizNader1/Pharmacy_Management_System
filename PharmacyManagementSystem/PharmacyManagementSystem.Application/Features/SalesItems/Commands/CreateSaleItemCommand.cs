using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record CreateSaleItemCommand(CreateSaleItemDto createSaleItemDto) : IRequest<int>;

    public class CreateSaleItemCommandHandler : IRequestHandler<CreateSaleItemCommand, int>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public CreateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<int> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
        {
            if (request.createSaleItemDto == null)
                throw new Exception("you should enter a valid values to each field");

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
