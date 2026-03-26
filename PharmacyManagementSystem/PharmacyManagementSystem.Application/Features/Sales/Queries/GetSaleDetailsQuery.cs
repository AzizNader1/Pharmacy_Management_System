using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetSaleDetailsQuery(int id) : IRequest<GetSaleDto>;

    public class GetSaleDetailsQueryHandler : IRequestHandler<GetSaleDetailsQuery, GetSaleDto>
    {
        private readonly ISalesRepository _salesRepository;

        public GetSaleDetailsQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<GetSaleDto> Handle(GetSaleDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid data for the id field, please enter a positive valid value.");

            var existingSale = await _salesRepository.GetSaleByIdAsync(request.id);
            if (existingSale == null)
                throw new Exception("there is no sales exists for this requested id");

            var saleItems = new List<GetSaleItemDto>();
            foreach (var item in existingSale.SaleItems)
            {
                saleItems.Add(new GetSaleItemDto
                {
                    BatchId = item.BatchId,
                    SaleItemId = item.SaleItemId,
                    ItemQuantity = item.ItemQuantity,
                    MedicineId = item.MedicineId,
                    SaleId = item.SaleId,
                    UnitPrice = item.UnitPrice
                });
            }


            var returnedSale = new GetSaleDto
            {
                SaleId = existingSale.SaleId,
                SaleItems = saleItems,
                SalesDate = existingSale.SalesDate,
                TotalAmount = existingSale.TotalAmount,
                UserId = existingSale.UserId
            };
            return returnedSale;

        }
    }
}
