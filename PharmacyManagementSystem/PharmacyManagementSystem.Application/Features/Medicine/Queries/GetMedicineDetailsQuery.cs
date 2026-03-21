using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetMedicineDetailsQuery(Guid id) : IRequest<GetMedicineDto>;

    public class GetMedicineDetailsQueryHandler : IRequestHandler<GetMedicineDetailsQuery, GetMedicineDto>
    {
        public Task<GetMedicineDto> Handle(GetMedicineDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
