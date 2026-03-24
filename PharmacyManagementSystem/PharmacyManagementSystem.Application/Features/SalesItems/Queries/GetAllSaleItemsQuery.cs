using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Queries
{
    public record GetAllSaleItemsQuery : IRequest<List<GetSaleItemDto>>;

    public class GetAllSaleItemsQueryHandler : IRequestHandler<GetAllSaleItemsQuery, List<GetSaleItemDto>>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public GetAllSaleItemsQueryHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<List<GetSaleItemDto>> Handle(GetAllSaleItemsQuery request, CancellationToken cancellationToken)
        {
            var saleItems = await _salesItemsRepository.GetAllSaleItemesAsync();
            if (saleItems == null || saleItems.Count() == 0)
                throw new Exception("there is no data exists in the database at this time, please try again later");

            var returnedSaleItems = new List<GetSaleItemDto>();
            foreach (var item in saleItems)
            {
                returnedSaleItems.Add(new GetSaleItemDto
                {
                    BatchId = item!.BatchId,
                    ItemQuantity = item!.ItemQuantity,
                    SaleItemId = item!.SaleItemId,
                    MedicineId = item!.MedicineId,
                    SaleId = item!.SaleId,
                    UnitPrice = item!.UnitPrice,
                });
            }
            ;
            return returnedSaleItems;

        }
    }
}
