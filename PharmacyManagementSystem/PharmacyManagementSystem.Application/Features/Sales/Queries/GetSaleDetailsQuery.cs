using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetSaleDetailsQuery(Guid id) : IRequest<GetSaleDto>;

    public class GetSaleDetailsQueryHandler : IRequestHandler<GetSaleDetailsQuery, GetSaleDto>
    {
        public Task<GetSaleDto> Handle(GetSaleDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
