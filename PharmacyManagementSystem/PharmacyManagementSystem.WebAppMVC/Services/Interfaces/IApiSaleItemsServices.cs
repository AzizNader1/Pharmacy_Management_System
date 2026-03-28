using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleItemsServices
    {
        Task? CreateSaleItemAsync(CreateSaleItemDto saleItem);
        Task? DeleteSaleItemAsync(int id);
        Task? UpdateSaleItemAsync(UpdateSaleItemDto saleItem);
        Task<GetSaleItemDto?> GetSaleItemByIdAsync(int id);
        Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesAsync();
        Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesBySaleIdAsync(int saleId);
    }
}
