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

            var existsMedicine = await _medicineRepository.GetMedicineByIdAsync(request.id);
            if (existsMedicine == null)
                throw new Exception("there is no medicines exists for this id");

            await _medicineRepository.DeleteMedicineAsync(existsMedicine)!;

            return true;
        }
    }
}
