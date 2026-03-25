using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs
{
    /// <summary>
    /// DTO for creating a new sale item (line item in a sale transaction).
    /// </summary>
    public class CreateSaleItemDto
    {
        /// <summary>
        /// ID of the sale transaction this item belongs to.
        /// </summary>
        [Required(ErrorMessage = "Sale ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sale ID must be a positive number.")]
        public int SaleId { get; set; }

        /// <summary>
        /// ID of the medicine being sold.
        /// </summary>
        [Required(ErrorMessage = "Medicine ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Medicine ID must be a positive number.")]
        public int MedicineId { get; set; }

        /// <summary>
        /// ID of the batch from which this item is being sold.
        /// </summary>
        [Required(ErrorMessage = "Batch ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Batch ID must be a positive number.")]
        public int BatchId { get; set; }

        /// <summary>
        /// Quantity of medicine units being sold.
        /// </summary>
        [Required(ErrorMessage = "Item quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int ItemQuantity { get; set; }

        /// <summary>
        /// Price per unit of the medicine at the time of sale.
        /// </summary>
        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Unit price must be between 0.01 and 999,999.99.")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Calculated total price for this line item (Quantity × Unit Price).
        /// </summary>
        public decimal TotalLinePrice => ItemQuantity * UnitPrice;
    }
}