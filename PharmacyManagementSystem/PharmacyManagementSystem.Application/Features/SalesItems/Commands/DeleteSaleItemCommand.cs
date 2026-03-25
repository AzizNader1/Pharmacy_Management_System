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

        public async Task<bool> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid value to the id field");

            var existsSaleItem = await _salesItemsRepository.GetSaleItemByIdAsync(request.id);
            if (existsSaleItem == null)
                throw new Exception("there is no sale items exists for this id");

            await _salesItemsRepository.DeleteSaleItemAsync(existsSaleItem)!;
            return true;
        }
    }
}
