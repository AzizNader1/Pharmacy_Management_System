using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record UpdateSaleCommand(Guid id, UpdateSaleDto UpdateSaleDto) : IRequest<GetSaleDto>;

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, GetSaleDto>
    {
        public Task<GetSaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
