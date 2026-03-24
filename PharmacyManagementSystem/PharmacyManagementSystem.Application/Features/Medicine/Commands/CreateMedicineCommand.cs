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

        public async Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateMedicineDto == null)
                throw new Exception("you should enter a valid data");

            var medicine = new Domain.Entities.Medicine
            {
                Name = request.CreateMedicineDto.Name,
                MedicinePrice = request.CreateMedicineDto.MedicinePrice,
                ReorderLevel = request.CreateMedicineDto.ReorderLevel,
                GenericName = request.CreateMedicineDto.GenericName,
                Description = request.CreateMedicineDto.Description,
                Category = request.CreateMedicineDto.Category
            };

            await _medicineRepository.CreateMedicineAsync(medicine)!;

            return medicine.MedicineId;
        }
    }
}
