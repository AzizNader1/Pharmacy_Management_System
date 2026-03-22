using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface ISalesItemsRepository
    {
        Task? CreateSaleItemAsync(SaleItem saleItem);
        Task? DeleteSaleItemAsync(int id);
        Task? UpdateSaleItemAsync(SaleItem saleItem);
        Task<SaleItem?> GetSaleItemByIdAsync(int id);
        Task<IEnumerable<SaleItem?>> GetAllSaleItemesAsync();
        Task<IEnumerable<SaleItem?>> GetAllSaleItemesBySaleIdAsync(int saleId);
    }
}
