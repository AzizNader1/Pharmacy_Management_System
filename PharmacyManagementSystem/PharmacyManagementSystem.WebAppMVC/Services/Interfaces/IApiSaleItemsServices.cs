using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleItemsServices
    {
        Task<GetSaleItemDto>? CreateSaleItemAsync(CreateSaleItemDto saleItem);
        Task<bool>? DeleteSaleItemAsync(int id);
        Task<GetSaleItemDto>? UpdateSaleItemAsync(int id, UpdateSaleItemDto saleItem);
        Task<GetSaleItemDto?> GetSaleItemByIdAsync(int id);
        Task<List<GetSaleItemDto?>> GetAllSaleItemesAsync();
        Task<List<GetSaleItemDto?>> GetAllSaleItemesBySaleIdAsync(int saleId);
    }
}
