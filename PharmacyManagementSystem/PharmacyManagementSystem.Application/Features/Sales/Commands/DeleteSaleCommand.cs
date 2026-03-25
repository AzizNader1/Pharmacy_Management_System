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

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value");

            var existsSale = await _salesRepository.GetSaleByIdAsync(request.id);
            if (existsSale == null)
                throw new Exception("there is no sale exists for this id");

            await _salesRepository.DeleteSaleAsync(existsSale)!;

            return true;
        }
    }
}
