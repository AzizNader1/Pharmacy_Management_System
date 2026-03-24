using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SalesItems.Queries
{
    public record GetAllSaleItemsBySaleIdQuery(int saleId) : IRequest<List<GetSaleItemDto>>;


    public class GetAllSaleItemsBySaleIdQueryHandler : IRequestHandler<GetAllSaleItemsBySaleIdQuery, List<GetSaleItemDto>>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public GetAllSaleItemsBySaleIdQueryHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<List<GetSaleItemDto>> Handle(GetAllSaleItemsBySaleIdQuery request, CancellationToken cancellationToken)
        {
            if (request.saleId <= 0)
                throw new Exception("you should enter a valid id value to get the correct data");

            var existingSaleItem = await _salesItemsRepository.GetAllSaleItemesBySaleIdAsync(request.saleId);
            if (existingSaleItem == null || existingSaleItem.Count() == 0)
                throw new Exception("there is no sale items exists in the database for this sales id at this time, please try again later");

            var returnedSaleItems = new List<GetSaleItemDto>();
            foreach (var item in existingSaleItem)
            {
                returnedSaleItems.Add(new GetSaleItemDto
                {
                    BatchId = item!.BatchId,
                    ItemQuantity = item!.ItemQuantity,
                    SaleId = item!.SaleId,
                    MedicineId = item!.MedicineId,
                    SaleItemId = item!.SaleItemId,
                    UnitPrice = item!.UnitPrice,
                });
            }
            ;
            return returnedSaleItems;
        }
    }
}
