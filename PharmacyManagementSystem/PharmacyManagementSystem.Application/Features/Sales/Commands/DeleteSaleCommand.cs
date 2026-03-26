using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record DeleteSaleCommand(int id) : IRequest<bool>;

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesItemsRepository _salesItemsRepository;

        public DeleteSaleCommandHandler(ISalesRepository salesRepository, ISalesItemsRepository salesItemsRepository)
        {
            _salesRepository = salesRepository;
            _salesItemsRepository = salesItemsRepository;
        }

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value");

            var existsSale = await _salesRepository.GetSaleByIdAsync(request.id);
            if (existsSale == null)
                throw new Exception("there is no sale exists for this id");

            var saleItemsIds = existsSale.SaleItems.Select(s => s.SaleId).ToList();

            await _salesRepository.DeleteSaleAsync(existsSale)!;
            foreach (var id in saleItemsIds)
            {
                var saleItem = await _salesItemsRepository.GetSaleItemByIdAsync(id);
                await _salesItemsRepository.DeleteSaleItemAsync(saleItem!)!;
            }

            return true;
        }
    }
}
