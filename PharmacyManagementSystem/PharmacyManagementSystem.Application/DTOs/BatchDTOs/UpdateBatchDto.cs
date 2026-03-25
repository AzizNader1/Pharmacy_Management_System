using PharmacyManagementSystem.Application.Attributes;
using PharmacyManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Application.DTOs.BatchDTOs
{
    /// <summary>
    /// DTO for updating an existing medicine batch.
    /// </summary>
    public class UpdateBatchDto
    {
        /// <summary>
        /// Unique identifier for the batch lot.
        /// </summary>
        [Required(ErrorMessage = "Batch number is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Batch number must be between 4 and 10 characters.")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Batch number must contain only letters and numbers.")]
        public string BatchNumber { get; set; } = string.Empty;

        /// <summary>
        /// Expiry date of the batch. Must be a future date.
        /// </summary>
        [Required(ErrorMessage = "Expiry date is required.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Expiry date must be a future date.")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Current quantity of medicine units in this batch.
        /// </summary>
        [Required(ErrorMessage = "Batch quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int BatchQuantity { get; set; }

        /// <summary>
        /// Purchase price per unit for this batch.
        /// </summary>
        [Required(ErrorMessage = "Purchase price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Purchase price must be between 0.01 and 999,999.99.")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Medicine category classification.
        /// </summary>
        [Required(ErrorMessage = "Category is required.")]
        [EnumDataType(typeof(MedicineCategories), ErrorMessage = "Invalid category value.")]
        public MedicineCategories Category { get; set; }

        /// <summary>
        /// ID of the medicine this batch belongs to.
        /// </summary>
        [Required(ErrorMessage = "Medicine ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Medicine ID must be a positive number.")]
        public int MedicineId { get; set; }
    }
}