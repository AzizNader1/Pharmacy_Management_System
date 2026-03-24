using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetAllMedicinesQuery : IRequest<List<GetMedicineDto>>;

    public class GetAllMedicinesQueryHandler : IRequestHandler<GetAllMedicinesQuery, List<GetMedicineDto>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetAllMedicinesQueryHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<List<GetMedicineDto>> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        {
            var medicines = await _medicineRepository.GetAllMedicinesAsync();
            if (medicines == null || medicines.Count() == 0)
                throw new Exception("there is no medicines exists in the database at this time");

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
