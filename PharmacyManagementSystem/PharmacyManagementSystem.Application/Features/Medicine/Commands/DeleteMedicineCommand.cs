using MediatR;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record DeleteMedicineCommand(Guid id) : IRequest<bool>;

    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, bool>
    {
        public Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
