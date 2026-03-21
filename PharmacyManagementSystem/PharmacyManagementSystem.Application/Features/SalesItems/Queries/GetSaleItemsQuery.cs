using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.Features.SaleItems.Queries
{
    public record GetSaleItemsDetailsQuery(Guid id) : IRequest<GetSaleItemDto>;

    public class GetSaleItemsDetailsQueryHandler : IRequestHandler<GetSaleItemsDetailsQuery, GetSaleItemDto>
    {
        public Task<GetSaleItemDto> Handle(GetSaleItemsDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
