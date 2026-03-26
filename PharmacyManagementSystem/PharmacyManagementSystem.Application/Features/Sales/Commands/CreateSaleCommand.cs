using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record CreateSaleCommand(CreateSaleDto createSaleDto) : IRequest<int>;

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IUserRepository _userRepository;

        public CreateSaleCommandHandler(ISalesRepository salesRepository, IUserRepository userRepository)
        {
            _salesRepository = salesRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.createSaleDto == null)
                throw new Exception("you should enter a valid data to each filed");

            if (request.createSaleDto.SalesDate < DateTime.UtcNow || request.createSaleDto.SalesDate > DateTime.UtcNow)
                throw new Exception("the sale date must be onle present not past or future, please enter a valid date");

            var existsUser = await _userRepository.GetUserByIdAsync(request.createSaleDto.UserId);
            if (existsUser == null)
                throw new Exception("there is no user exists for that user id that you try to use, please use a valid user id");

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
