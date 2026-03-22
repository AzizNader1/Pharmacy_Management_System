using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record DeleteSaleItemCommand(int id) : IRequest<bool>;

    public class DeleteSaleItemCommandHandler : IRequestHandler<DeleteSaleItemCommand, bool>
    {
        private readonly ISalesItemsRepository _salesItemsRepository;

        public DeleteSaleItemCommandHandler(ISalesItemsRepository salesItemsRepository)
        {
            _salesItemsRepository = salesItemsRepository;
        }

        public Task<bool> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
