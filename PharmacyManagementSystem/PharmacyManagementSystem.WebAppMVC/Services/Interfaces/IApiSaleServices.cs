using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiSaleServices
    {
        Task<GetSaleDto>? CreateSaleAsync(CreateSaleDto sale);
        Task<GetSaleDto>? CreateSaleWithItemsAsync(CreateSaleDto sale, List<CreateSaleItemDto> saleItems);
        Task<bool>? DeleteSaleAsync(int id);
        Task<GetSaleDto>? UpdateSaleAsync(int id, UpdateSaleDto sale);
        Task<GetSaleDto?> GetSaleByIdAsync(int id);
        Task<List<GetSaleDto?>> GetAllSalesAsync();
        Task<List<GetSaleDto?>> GetAllSalesByUserIdAsync(int userId);
        Task<List<GetSaleDto?>> GetAllSalesByUserNameAsync(string userName);
    }
}
