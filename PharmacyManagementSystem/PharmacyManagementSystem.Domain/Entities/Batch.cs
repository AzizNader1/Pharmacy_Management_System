using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Domain.Entities
{
    public class Batch
    {
        public Guid BatchId { get; set; }
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public int BatchQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public MedicineCategories Category { get; set; } = MedicineCategories.None;
        public Guid MedicineId { get; set; }

        // Navigation Property
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual ICollection<SaleItem> SaleItems { get; set; } = [];
    }
}
