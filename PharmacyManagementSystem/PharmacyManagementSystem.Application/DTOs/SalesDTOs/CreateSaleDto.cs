namespace PharmacyManagementSystem.Application.DTOs.SalesDTOs
{
    public class CreateSaleDto
    {
        public int UserId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
