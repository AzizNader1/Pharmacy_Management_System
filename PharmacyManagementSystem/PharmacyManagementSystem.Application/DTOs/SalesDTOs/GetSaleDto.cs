using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;

namespace PharmacyManagementSystem.Application.DTOs.SalesDTOs
{
    public class GetSaleDto
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<GetSaleItemDto> SaleItems { get; set; } = [];
        public string? Message { get; set; }

    }
}
