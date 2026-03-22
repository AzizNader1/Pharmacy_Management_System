using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.DTOs.BatchDTOs
{
    public class UpdateBatchDto
    {
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public int BatchQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public MedicineCategories Category { get; set; } = MedicineCategories.None;
        public int MedicineId { get; set; }
    }
}
