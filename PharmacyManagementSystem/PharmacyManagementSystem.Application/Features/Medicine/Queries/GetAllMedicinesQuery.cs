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
            throw new NotImplementedException();
        }
    }
}
