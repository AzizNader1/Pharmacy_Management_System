using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetMedicinesByCategoryQuery(string category) : IRequest<List<GetMedicineDto>>;

    public class GetMedicinesByCategoryQueryHandler : IRequestHandler<GetMedicinesByCategoryQuery, List<GetMedicineDto>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicinesByCategoryQueryHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<List<GetMedicineDto>> Handle(GetMedicinesByCategoryQuery request, CancellationToken cancellationToken)
        {
            if (request.category == null)
                throw new Exception("you should enter a valid category name");

            var medicines = await _medicineRepository.GetAllMedicinesByCategoryAsync(request.category);
            if (medicines == null || medicines.Count() == 0)
                throw new Exception("there is no medicines exists in the database for that requested category");

            var returnedMedicines = new List<GetMedicineDto>();
            foreach (var medicine in medicines)
            {
                returnedMedicines.Add(new GetMedicineDto
                {
                    Category = medicine!.Category,
                    Description = medicine!.Description,
                    GenericName = medicine!.GenericName,
                    MedicineId = medicine!.MedicineId,
                    MedicinePrice = medicine!.MedicinePrice,
                    Name = medicine!.Name,
                    ReorderLevel = medicine!.ReorderLevel
                });
            }
            return returnedMedicines;
        }
    }
}
