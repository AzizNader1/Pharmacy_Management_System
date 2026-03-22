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
            throw new NotImplementedException();
        }
    }
}
