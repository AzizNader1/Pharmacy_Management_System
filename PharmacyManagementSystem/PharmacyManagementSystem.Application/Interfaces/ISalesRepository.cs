using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface ISalesRepository
    {
        Task<Guid> CreateSaleAsync(CreateSaleDto createSaleDto);
        Task<Guid> DeleteSaleAsync(Guid id);
        Task<Guid> UpdateSaleAsync(Guid id, UpdateSaleDto updateSaleDto);
        Task<GetSaleDto> GetSaleByIdAsync(Guid id);
        Task<IEnumerable<GetSaleDto>> GetAllSalesAsync();
        Task<IEnumerable<GetSaleDto>> GetAllSalesByUserIdAsync();
        Task<IEnumerable<GetSaleDto>> GetAllSalesByUserNameAsync();
    }
}
