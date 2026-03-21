using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        public Task<Guid> CreateBatchAsync(CreateBatchDto createBatchDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteBatchAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetBatchDto>> GetAllBatchesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetBatchDto> GetBatchByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateBatchAsync(Guid id, UpdateBatchDto updateBatchDto)
        {
            throw new NotImplementedException();
        }
    }
}
