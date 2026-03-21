using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record UpdateSaleItemCommand(Guid id, UpdateSaleItemDto UpdateSaleItemDto) : IRequest<GetSaleItemDto>;

    public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand, GetSaleItemDto>
    {
        public Task<GetSaleItemDto> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
