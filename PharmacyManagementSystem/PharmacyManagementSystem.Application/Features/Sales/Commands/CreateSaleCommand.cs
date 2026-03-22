using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record CreateSaleCommand(CreateSaleDto CreateSaleDto) : IRequest<int>;

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISalesRepository _salesRepository;

        public CreateSaleCommandHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
