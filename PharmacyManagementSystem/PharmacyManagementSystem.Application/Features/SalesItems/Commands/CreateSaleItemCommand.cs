using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record CreateSaleItemCommand(CreateSaleItemDto CreateSaleItemDto) : IRequest<int>;

    public class CreateSaleItemCommandHandler : IRequestHandler<CreateSaleItemCommand, int>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public CreateSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public Task<int> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
