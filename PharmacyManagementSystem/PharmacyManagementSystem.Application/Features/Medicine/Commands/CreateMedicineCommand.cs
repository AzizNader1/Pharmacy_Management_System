using MediatR;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record CreateMedicineCommand(CreateMedicineDto CreateMedicineDto) : IRequest<Guid>;

    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, Guid>
    {
        public Task<Guid> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
