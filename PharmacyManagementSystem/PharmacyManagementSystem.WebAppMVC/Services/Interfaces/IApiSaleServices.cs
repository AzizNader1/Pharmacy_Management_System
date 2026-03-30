using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleServices
    {
        Task<GetSaleDto>? CreateSaleAsync(CreateSaleDto sale);
        Task<bool>? DeleteSaleAsync(int id);
        Task<GetSaleDto>? UpdateSaleAsync(int id, UpdateSaleDto sale);
        Task<GetSaleDto?> GetSaleByIdAsync(int id);
        Task<IEnumerable<GetSaleDto?>> GetAllSalesAsync();
        Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserIdAsync(int userId);
        Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserNameAsync(string userName);
    }
}
