using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record UpdateMedicineCommand(Guid id, UpdateMedicineDto UpdateMedicineDto) : IRequest<GetMedicineDto>;

    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, GetMedicineDto>
    {
        public Task<GetMedicineDto> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
