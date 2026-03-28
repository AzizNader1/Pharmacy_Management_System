using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleServices
    {
        Task? CreateSaleAsync(CreateSaleDto sale);
        Task? DeleteSaleAsync(int id);
        Task? UpdateSaleAsync(UpdateSaleDto sale);
        Task<GetSaleDto?> GetSaleByIdAsync(int id);
        Task<IEnumerable<GetSaleDto?>> GetAllSalesAsync();
        Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserIdAsync(int userId);
        Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserNameAsync(string userName);
    }
}
