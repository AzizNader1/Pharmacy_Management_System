using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record UpdateSaleItemCommand(int id, UpdateSaleItemDto UpdateSaleItemDto) : IRequest<GetSaleItemDto>;

    public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand, GetSaleItemDto>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public UpdateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public Task<GetSaleItemDto> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
