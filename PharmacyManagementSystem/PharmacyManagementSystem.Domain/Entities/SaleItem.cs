using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Domain.Entities
{
    public class SaleItem
    {
        public Guid SaleItemId { get; set; } // Unique identifier for each sale item
        public Guid SaleId { get; set; }       // FK to Sale
        public Guid MedicineId { get; set; }   // FK to Medicine (For quick reference/reporting)
        public Guid BatchId { get; set; }      // FK to Batch (The specific stock deducted)
        public int ItemQuantity { get; set; }

        // Price at the moment of sale (prices might change later, but invoice stays same)
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
