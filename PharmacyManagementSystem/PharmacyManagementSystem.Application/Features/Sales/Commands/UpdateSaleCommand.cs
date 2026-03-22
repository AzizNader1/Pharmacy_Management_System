using MediatR;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Sale.Commands
{
    public record UpdateSaleCommand(int id, UpdateSaleDto UpdateSaleDto) : IRequest<GetSaleDto>;

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, GetSaleDto>
    {
        private readonly ISalesRepository _salesRepository;

        public UpdateSaleCommandHandler(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Task<GetSaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
