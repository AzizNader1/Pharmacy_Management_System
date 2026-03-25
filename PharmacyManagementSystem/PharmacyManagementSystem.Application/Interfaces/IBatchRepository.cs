using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IBatchRepository
    {
        Task? CreateBatchAsync(Batch batch);
        Task? DeleteBatchAsync(Batch batch);
        Task? UpdateBatchAsync(Batch batch);
        Task<Batch?> GetBatchByIdAsync(int id);
        Task<IEnumerable<Batch?>> GetAllBatchesAsync();
    }
}
