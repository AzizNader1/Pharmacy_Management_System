using MediatR;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record DeleteSaleCommand(Guid id) : IRequest<bool>;

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        public Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
