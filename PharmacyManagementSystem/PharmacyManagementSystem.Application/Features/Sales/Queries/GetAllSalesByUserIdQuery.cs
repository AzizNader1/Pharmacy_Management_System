using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sales.Queries
{
    public record GetAllSalesByUserIdQuery(int userId) : IRequest<List<GetSaleDto>>;

    public class GetAllSalesByUserIdQueryHandler : IRequestHandler<GetAllSalesByUserIdQuery, List<GetSaleDto>>
    {
        private readonly ISalesRepository _salesRepository;

        public GetAllSalesByUserIdQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<GetSaleDto>> Handle(GetAllSalesByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.userId == 0)
                throw new Exception("you should enter a valid value to the id");

            var existingSaleForUser = await _salesRepository.GetAllSalesByUserIdAsync(request.userId);
            if (existingSaleForUser == null)
                throw new Exception("there is no exists user for this requested id");

            if (existingSaleForUser.Count() == 0 || existingSaleForUser == null)
                throw new Exception("there is no sales exists to this user yet, try to add sales first to that user");

            var wantedSaleItems = new List<GetSaleItemDto>();

            foreach (var item in existingSaleForUser.SelectMany(s => s!.SaleItems))
            {
                wantedSaleItems.Add(new GetSaleItemDto
                {
                    BatchId = item!.BatchId,
                    ItemQuantity = item!.ItemQuantity,
                    SaleItemId = item!.SaleItemId,
                    MedicineId = item!.MedicineId,
                    SaleId = item!.SaleId,
                    UnitPrice = item!.UnitPrice,
                });
            }

            var returnedSales = new List<GetSaleDto>();
            foreach (var item in existingSaleForUser)
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
