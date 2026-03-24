using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetMedicineByNameQuery(string name) : IRequest<GetMedicineDto>;

    public class GetMedicineByNameQueryHandler : IRequestHandler<GetMedicineByNameQuery, GetMedicineDto>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicineByNameQueryHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<GetMedicineDto> Handle(GetMedicineByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.name == null)
                throw new Exception("you should enter a valid medicine name");

            var medicine = await _medicineRepository.GetMedicineByNameAsync(request.name);
            if (medicine == null)
                throw new Exception("there is no medicine avalible inside the database for this requested name");

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
