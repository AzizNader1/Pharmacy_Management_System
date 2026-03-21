using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Queries
{
    public record GetAllSaleItemsQuery : IRequest<List<GetSaleItemDto>>;

    public class GetAllSaleItemsQueryHandler : IRequestHandler<GetAllSaleItemsQuery, List<GetSaleItemDto>>
    {
        public async Task<List<GetSaleItemDto>> Handle(GetAllSaleItemsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
