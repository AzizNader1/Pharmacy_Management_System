using MediatR;

namespace PharmacyManagementSystem.Application.Features.SaleItem.Commands
{
    public record DeleteSaleItemCommand(Guid id) : IRequest<bool>;

    public class DeleteSaleItemCommandHandler : IRequestHandler<DeleteSaleItemCommand, bool>
    {
        public Task<bool> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
