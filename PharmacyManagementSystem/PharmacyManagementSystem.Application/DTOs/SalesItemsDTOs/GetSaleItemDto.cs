namespace PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs
{
    public class GetSaleItemDto
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int BatchId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalLinePrice => ItemQuantity * UnitPrice;

        public string? Message { get; set; }
    }
}
