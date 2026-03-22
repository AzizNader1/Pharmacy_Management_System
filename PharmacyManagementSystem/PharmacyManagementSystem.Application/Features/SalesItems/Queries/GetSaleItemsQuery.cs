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

        public Task<GetSaleItemDto> Handle(GetSaleItemsDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
