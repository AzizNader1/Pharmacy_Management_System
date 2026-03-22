using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record CreateMedicineCommand(CreateMedicineDto CreateMedicineDto) : IRequest<int>;

    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
    {
        private readonly IMedicineRepository _medicineRepository;

        public CreateMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
