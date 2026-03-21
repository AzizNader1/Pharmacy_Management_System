using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface ISalesItemsRepository
    {
        Task<Guid> CreateSaleItemAsync(CreateSaleItemDto createSaleItemDto);
        Task<Guid> DeleteSaleItemAsync(Guid id);
        Task<Guid> UpdateSaleItemAsync(Guid id, UpdateSaleItemDto updateSaleItemDto);
        Task<GetSaleItemDto> GetSaleItemByIdAsync(Guid id);
        Task<IEnumerable<GetSaleItemDto>> GetAllSaleItemesAsync();
        Task<IEnumerable<GetSaleItemDto>> GetAllSaleItemesBySaleIdAsync();
    }
}
