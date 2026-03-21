using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Domain.Entities
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MedicinePrice { get; set; }
        public int ReorderLevel { get; set; } // Alert when stock goes below this
        public MedicineCategories Category { get; set; } = MedicineCategories.None;
        public int TotalStock => Batches.Sum(b => b.BatchQuantity);

        // Navigation Property
        public virtual ICollection<Batch> Batches { get; set; } = [];
    }
}
