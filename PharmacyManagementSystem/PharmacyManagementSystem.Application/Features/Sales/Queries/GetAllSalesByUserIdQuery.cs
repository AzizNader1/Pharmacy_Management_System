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
        private readonly IUserRepository _userRepository;

        public GetAllSalesByUserIdQueryHandler(ISalesRepository salesRepository, IUserRepository userRepository)
        {
            _salesRepository = salesRepository;
            _userRepository = userRepository;
        }

        public async Task<List<GetSaleDto>> Handle(GetAllSalesByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
                throw new Exception("you should enter a valid value to the id");

            var existsUser = await _userRepository.GetUserByIdAsync(request.userId);
            if (existsUser == null)
                throw new Exception("there is no exists user in the database for this user id");

            var existingSaleForUser = await _salesRepository.GetAllSalesByUserIdAsync(request.userId);
            if (existingSaleForUser == null)
                throw new Exception("there is no exists sales for this requested user");

            var wantedSaleItems = new List<GetSaleItemDto>();
            var existsSaleItemsForUser = existingSaleForUser.SelectMany(s => s!.SaleItems).ToList();
            foreach (var item in existsSaleItemsForUser)
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
                    SaleItems = wantedSaleItems.Count() == 0 ? [] : wantedSaleItems.Where(s => s.SaleId == item.SaleId).ToList(),
                });
            }
            ;
            return returnedSales;
        }
    }
}
