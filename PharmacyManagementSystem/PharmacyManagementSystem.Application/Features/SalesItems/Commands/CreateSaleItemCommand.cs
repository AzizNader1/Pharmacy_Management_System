using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record CreateSaleItemCommand(CreateSaleItemDto CreateSaleItemDto) : IRequest<Guid>;

    public class CreateSaleItemCommandHandler : IRequestHandler<CreateSaleItemCommand, Guid>
    {
        public Task<Guid> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
