using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetAllSalesQuery : IRequest<List<GetSaleDto>>;

    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetSaleDto>>
    {
        private readonly ISalesRepository _salesRepository;

        public GetAllSalesQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<GetSaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var allSales = await _salesRepository.GetAllSalesAsync();
            if (allSales.Count() == 0 || allSales == null)
                throw new Exception("there is no sales exists in the database at this time");

            var wantedSaleItems = new List<GetSaleItemDto>();
            foreach (var item in allSales.SelectMany(s => s!.SaleItems))
            {
                wantedSaleItems.Add(new GetSaleItemDto
                {
                    BatchId = item.BatchId,
                    ItemQuantity = item.ItemQuantity,
                    SaleItemId = item.SaleItemId,
                    MedicineId = item.MedicineId,
                    SaleId = item.SaleId,
                    UnitPrice = item.UnitPrice,
                });
            }

            var returnedSales = new List<GetSaleDto>();
            foreach (var item in allSales)
            {
                returnedSales.Add(new GetSaleDto
                {
                    SaleId = item!.SaleId,
                    SalesDate = item!.SalesDate,
                    TotalAmount = item!.TotalAmount,
                    UserId = item!.UserId,
                    SaleItems = wantedSaleItems,
                });
            }
            ;
            return returnedSales;
        }
    }
}
