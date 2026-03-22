namespace PharmacyManagementSystem.Application.DTOs.SalesDTOs
{
    public class UpdateSaleDto
    {
        public int UserId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
