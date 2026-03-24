using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sales.Queries
{
    public record GetAllSalesByUserNameQuery(string usreName) : IRequest<List<GetSaleDto>>;

    public class GetAllSalesByUserNameQueryHandler : IRequestHandler<GetAllSalesByUserNameQuery, List<GetSaleDto>>
    {
        private readonly ISalesRepository _salesRepository;

        public GetAllSalesByUserNameQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<GetSaleDto>> Handle(GetAllSalesByUserNameQuery request, CancellationToken cancellationToken)
        {
            if (request.usreName == null)
                throw new Exception("you should enter a valid data for the user name field");

            var existingSaleForUser = await _salesRepository.GetAllSalesByUserNameAsync(request.usreName);
            if (existingSaleForUser == null)
                throw new Exception("ther is no users exists for this name");

            if (existingSaleForUser.Count() == 0 || existingSaleForUser == null)
                throw new Exception("there is no sales avalible for this user");

            var wantedSaleItems = new List<GetSaleItemDto>();

            foreach (var item in existingSaleForUser.SelectMany(s => s!.SaleItems))
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
