using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItems.Queries
{
    public record GetSaleItemsDetailsQuery(int id) : IRequest<GetSaleItemDto>;

    public class GetSaleItemsDetailsQueryHandler : IRequestHandler<GetSaleItemsDetailsQuery, GetSaleItemDto>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public GetSaleItemsDetailsQueryHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<GetSaleItemDto> Handle(GetSaleItemsDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value to get the correct data");

            var existingSaleItem = await _salesItemsRepository.GetSaleItemByIdAsync(request.id);
            if (existingSaleItem == null)
                throw new Exception("there is no sale items exists in the database for this id at this time, please try again later");

            var returnedSaleItem = new GetSaleItemDto
            {
                BatchId = existingSaleItem.BatchId,
                SaleItemId = existingSaleItem.SaleItemId,
                UnitPrice = existingSaleItem.UnitPrice,
                ItemQuantity = existingSaleItem.ItemQuantity,
                MedicineId = existingSaleItem.MedicineId,
                SaleId = existingSaleItem.SaleId,
            };
            return returnedSaleItem;
        }
    }
}