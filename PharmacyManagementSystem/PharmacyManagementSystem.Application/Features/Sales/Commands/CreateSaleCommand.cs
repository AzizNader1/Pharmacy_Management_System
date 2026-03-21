using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record CreateSaleCommand(CreateSaleDto CreateSaleDto) : IRequest<Guid>;

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        public Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
