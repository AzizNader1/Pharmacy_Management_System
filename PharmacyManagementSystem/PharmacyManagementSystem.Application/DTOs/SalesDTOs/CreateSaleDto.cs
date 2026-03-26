using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Application.DTOs.SalesDTOs
{
    /// <summary>
    /// DTO for creating a new sales transaction.
    /// </summary>
    public class CreateSaleDto
    {
        /// <summary>
        /// ID of the user (cashier) processing the sale.
        /// </summary>
        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number.")]
        public int UserId { get; set; }

        /// <summary>
        /// Date and time when the sale occurred.
        /// </summary>
        [Required(ErrorMessage = "Sales date is required.")]
        [DataType(DataType.Date)]
        public DateTime SalesDate { get; set; }

        /// <summary>
        /// Total amount of the sale transaction.
        /// </summary>
        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0, 99999999.99, ErrorMessage = "Total amount must be between 0 and 99,999,999.99.")]
        public decimal TotalAmount { get; set; }
    }
}