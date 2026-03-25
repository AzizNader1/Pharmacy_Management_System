using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface ISalesRepository
    {
        Task? CreateSaleAsync(Sale sale);
        Task? DeleteSaleAsync(Sale sale);
        Task? UpdateSaleAsync(Sale sale);
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<IEnumerable<Sale?>> GetAllSalesAsync();
        Task<IEnumerable<Sale?>> GetAllSalesByUserIdAsync(int userId);
        Task<IEnumerable<Sale?>> GetAllSalesByUserNameAsync(string userName);
    }
}
