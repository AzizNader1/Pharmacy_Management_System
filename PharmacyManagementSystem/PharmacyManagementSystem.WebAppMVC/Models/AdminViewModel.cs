using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.DTOs.SalesDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Models
{
    public class AdminViewModel
    {
        public List<GetBatchDto> Batchs { get; set; } = [];
        public List<GetMedicineDto> Medicines { get; set; } = [];
        public List<GetSaleDto> Sales { get; set; } = [];
    }
}
