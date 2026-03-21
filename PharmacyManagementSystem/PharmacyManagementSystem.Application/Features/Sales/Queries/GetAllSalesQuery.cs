using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetAllSalesQuery : IRequest<List<GetSaleDto>>;

    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetSaleDto>>
    {
        public async Task<List<GetSaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
