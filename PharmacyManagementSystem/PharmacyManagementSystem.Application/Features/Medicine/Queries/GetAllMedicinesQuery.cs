using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.Application.Features.Medicine.Queries
{
    public record GetAllMedicinesQuery : IRequest<List<GetMedicineDto>>;

    public class GetAllMedicinesQueryHandler : IRequestHandler<GetAllMedicinesQuery, List<GetMedicineDto>>
    {
        public async Task<List<GetMedicineDto>> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
