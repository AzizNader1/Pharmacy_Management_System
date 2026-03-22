using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record UpdateMedicineCommand(int id, UpdateMedicineDto UpdateMedicineDto) : IRequest<GetMedicineDto>;

    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, GetMedicineDto>
    {
        private readonly IMedicineRepository _medicineRepository;

        public UpdateMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<GetMedicineDto> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
