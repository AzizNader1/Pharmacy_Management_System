using PharmacyManagementSystem.WebAppMVC.Models;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IReportsService
    {
        Task<ReportsViewModel> BuildReportsViewModelAsync();
    }
}
