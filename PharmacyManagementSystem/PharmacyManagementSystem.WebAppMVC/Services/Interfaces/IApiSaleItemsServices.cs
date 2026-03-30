using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleItemsServices
    {
        Task<GetSaleItemDto>? CreateSaleItemAsync(CreateSaleItemDto saleItem);
        Task<bool>? DeleteSaleItemAsync(int id);
        Task<GetSaleItemDto>? UpdateSaleItemAsync(int id, UpdateSaleItemDto saleItem);
        Task<GetSaleItemDto?> GetSaleItemByIdAsync(int id);
        Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesAsync();
        Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesBySaleIdAsync(int saleId);
    }
}
