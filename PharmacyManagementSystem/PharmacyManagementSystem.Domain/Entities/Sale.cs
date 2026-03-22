namespace PharmacyManagementSystem.Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<SaleItem> SaleItems { get; set; } = [];

    }
}
