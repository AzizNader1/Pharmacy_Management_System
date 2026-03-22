using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetSaleDetailsQuery(int id) : IRequest<GetSaleDto>;

    public class GetSaleDetailsQueryHandler : IRequestHandler<GetSaleDetailsQuery, GetSaleDto>
    {
        private readonly ISalesRepository _salesRepository;

        public GetSaleDetailsQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Task<GetSaleDto> Handle(GetSaleDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
