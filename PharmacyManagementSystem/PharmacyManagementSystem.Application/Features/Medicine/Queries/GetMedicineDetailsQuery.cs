using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetMedicineDetailsQuery(int id) : IRequest<GetMedicineDto>;

    public class GetMedicineDetailsQueryHandler : IRequestHandler<GetMedicineDetailsQuery, GetMedicineDto>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicineDetailsQueryHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<GetMedicineDto> Handle(GetMedicineDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
