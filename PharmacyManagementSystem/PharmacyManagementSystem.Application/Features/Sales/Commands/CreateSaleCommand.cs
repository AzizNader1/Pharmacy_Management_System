using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record CreateSaleCommand(CreateSaleDto createSaleDto) : IRequest<int>;

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISalesRepository _salesRepository;

        public CreateSaleCommandHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.createSaleDto == null)
                throw new Exception("you should enter a valid data to each filed");

            var sale = new Domain.Entities.Sale
            {
                SalesDate = DateTime.UtcNow,
                TotalAmount = request.createSaleDto.TotalAmount,
                UserId = request.createSaleDto.UserId,
            };

            await _salesRepository.CreateSaleAsync(sale)!;

            return sale.SaleId;
        }
    }
}
