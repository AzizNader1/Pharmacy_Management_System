using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record DeleteSaleCommand(int id) : IRequest<bool>;

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISalesRepository _salesRepository;

        public DeleteSaleCommandHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
