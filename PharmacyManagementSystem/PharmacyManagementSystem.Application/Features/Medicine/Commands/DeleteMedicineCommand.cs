using MediatR;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Application.Features.Medicine.Commands
{
    public record DeleteMedicineCommand(int id) : IRequest<bool>;

    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, bool>
    {
        private readonly IMedicineRepository _medicineRepository;

        public DeleteMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
