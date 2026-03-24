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

        public async Task<GetMedicineDto> Handle(GetMedicineDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value");

            var medicine = await _medicineRepository.GetMedicineByIdAsync(request.id);
            if (medicine == null)
                throw new Exception("there is no medicine avalible in the database for this requested id");

            var returnedMedicine = new GetMedicineDto
            {
                Category = medicine.Category,
                Description = medicine.Description,
                GenericName = medicine.GenericName,
                MedicineId = medicine.MedicineId,
                MedicinePrice = medicine.MedicinePrice,
                Name = medicine.Name,
                ReorderLevel = medicine.ReorderLevel
            };
            return returnedMedicine;

        }
    }
}
