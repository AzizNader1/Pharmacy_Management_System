using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record UpdateMedicineCommand(int id, UpdateMedicineDto updateMedicineDto) : IRequest<GetMedicineDto>;

    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, GetMedicineDto>
    {
        private readonly IMedicineRepository _medicineRepository;

        public UpdateMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<GetMedicineDto> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0 || request.updateMedicineDto == null)
                throw new Exception("you should enter a valid data");

            var existingMedicine = await _medicineRepository.GetMedicineByIdAsync(request.id);
            if (existingMedicine == null)
                throw new Exception("there is no medicines inside the database for this requested id");

            existingMedicine.ReorderLevel = request.updateMedicineDto.ReorderLevel;
            existingMedicine.MedicinePrice = request.updateMedicineDto.MedicinePrice;

            await _medicineRepository.UpdateMedicineAsync(existingMedicine)!;

            var newMedicineData = new GetMedicineDto
            {
                Category = existingMedicine.Category,
                Description = existingMedicine.Description,
                GenericName = existingMedicine.GenericName,
                MedicineId = existingMedicine.MedicineId,
                MedicinePrice = existingMedicine.MedicinePrice,
                Name = existingMedicine.Name,
                ReorderLevel = existingMedicine.ReorderLevel
            };
            return newMedicineData;
        }
    }
}
