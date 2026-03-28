using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiBatchServices
    {
        Task? CreateBatchAsync(CreateBatchDto batch);
        Task? DeleteBatchAsync(int id);
        Task? UpdateBatchAsync(UpdateBatchDto batch);
        Task<GetBatchDto?> GetBatchByIdAsync(int id);
        Task<IEnumerable<GetBatchDto?>> GetAllBatchesAsync();
    }
}
