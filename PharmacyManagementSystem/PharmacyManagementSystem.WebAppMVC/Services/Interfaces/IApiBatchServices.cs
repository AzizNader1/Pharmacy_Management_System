using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiBatchServices
    {
        Task<GetBatchDto>? CreateBatchAsync(CreateBatchDto batch);
        Task<bool>? DeleteBatchAsync(int id);
        Task<GetBatchDto> UpdateBatchAsync(int id, UpdateBatchDto batch);
        Task<GetBatchDto?> GetBatchByIdAsync(int id);
        Task<IEnumerable<GetBatchDto?>> GetAllBatchesAsync();
    }
}
