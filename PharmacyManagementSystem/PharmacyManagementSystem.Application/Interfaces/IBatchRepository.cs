using PharmacyManagementSystem.Application.DTOs.BatchDTOs;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IBatchRepository
    {
        Task<Guid> CreateBatchAsync(CreateBatchDto createBatchDto);
        Task<Guid> DeleteBatchAsync(Guid id);
        Task<Guid> UpdateBatchAsync(Guid id, UpdateBatchDto updateBatchDto);
        Task<GetBatchDto> GetBatchByIdAsync(Guid id);
        Task<IEnumerable<GetBatchDto>> GetAllBatchesAsync();
    }
}
