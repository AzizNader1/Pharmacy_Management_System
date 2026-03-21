using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class SalesItemsRepository : ISalesItemsRepository
    {
        public Task<Guid> CreateSaleItemAsync(CreateSaleItemDto createSaleItemDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteSaleItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleItemDto>> GetAllSaleItemesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleItemDto>> GetAllSaleItemesBySaleIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetSaleItemDto> GetSaleItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateSaleItemAsync(Guid id, UpdateSaleItemDto updateSaleItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
