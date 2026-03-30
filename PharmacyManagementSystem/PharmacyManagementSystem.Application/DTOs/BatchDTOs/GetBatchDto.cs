using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.DTOs.BatchDTOs
{
    public class GetBatchDto
    {
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public int BatchQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public MedicineCategories Category { get; set; }
        public int MedicineId { get; set; }

        public string? Message { get; set; }
    }
}
