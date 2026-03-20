namespace PharmacyManagementSystem.Domain.Entities
{
    public class Sale
    {
        public Guid SalesId { get; set; }
        public Guid UserId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }

        public User User { get; set; } = null!;
        public ICollection<SaleItem> SaleItems { get; set; } = [];

    }
}
