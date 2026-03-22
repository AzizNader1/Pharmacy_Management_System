using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Domain.Entities
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int BatchId { get; set; }
        public int ItemQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalLinePrice => ItemQuantity * UnitPrice;

        // Navigation Properties
        public virtual Sale Sale { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual Batch Batch { get; set; } = null!;
    }
}
