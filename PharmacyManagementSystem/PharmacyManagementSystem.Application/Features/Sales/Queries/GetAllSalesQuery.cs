using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Queries
{
    public record GetAllSalesQuery : IRequest<List<GetSaleDto>>;

    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetSaleDto>>
    {
        private readonly ISalesRepository _salesRepository;

        public GetAllSalesQueryHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<GetSaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
