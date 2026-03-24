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

        public async Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new Exception("you should enter a valid id value");

            await _medicineRepository.DeleteMedicineAsync(request.id)!;

            return true;
        }
    }
}
