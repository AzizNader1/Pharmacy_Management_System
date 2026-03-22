namespace PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs
{
    public class UpdateSaleItemDto
    {
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int BatchId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
