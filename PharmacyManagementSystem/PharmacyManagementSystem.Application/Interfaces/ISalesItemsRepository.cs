using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface ISalesItemsRepository
    {
        Task? CreateSaleItemAsync(SaleItem saleItem);
        Task? DeleteSaleItemAsync(SaleItem saleItem);
        Task? UpdateSaleItemAsync(SaleItem saleItem);
        Task<SaleItem?> GetSaleItemByIdAsync(int id);
        Task<IEnumerable<SaleItem?>> GetAllSaleItemesAsync();
        Task<IEnumerable<SaleItem?>> GetAllSaleItemesBySaleIdAsync(int saleId);
    }
}
