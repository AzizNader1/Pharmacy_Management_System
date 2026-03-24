using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record UpdateSaleCommand(int id, UpdateSaleDto updateSaleDto) : IRequest<GetSaleDto>;

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, GetSaleDto>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesItemsRepository _salesItemsRepository;

        public UpdateSaleCommandHandler(ISalesRepository salesRepository, ISalesItemsRepository salesItemsRepository)
        {
            _salesRepository = salesRepository;
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<GetSaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0 || request.updateSaleDto == null)
                throw new Exception("you should enter a valid data values");

            var existingSale = await _salesRepository.GetSaleByIdAsync(request.id);
            if (existingSale == null)
                throw new Exception("there is no sale record for this requsted id");

            existingSale.TotalAmount = request.updateSaleDto.TotalAmount;

            await _salesRepository.UpdateSaleAsync(existingSale)!;

            var saleItems = new List<GetSaleItemDto>();
            foreach (var item in existingSale.SaleItems)
            {
                saleItems.Add(new GetSaleItemDto
                {
                    BatchId = item.BatchId,
                    SaleId = item.SaleId,
                    ItemQuantity = item.ItemQuantity,
                    MedicineId = item.MedicineId,
                    SaleItemId = item.SaleItemId,
                    UnitPrice = item.UnitPrice
                });
            }

            var newSaleData = new GetSaleDto
            {
                SaleId = existingSale.SaleId,
                SalesDate = existingSale.SalesDate,
                TotalAmount = existingSale.TotalAmount,
                UserId = existingSale.UserId,
                SaleItems = saleItems
            };

            return newSaleData;

        }
    }
}
