using PharmacyManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Application.DTOs.MedicineDTOs
{
    /// <summary>
    /// DTO for creating a new medicine in the pharmacy system.
    /// </summary>
    public class CreateMedicineDto
    {
        /// <summary>
        /// Brand name of the medicine (e.g., "Panadol", "Augmentin").
        /// </summary>
        [Required(ErrorMessage = "Medicine name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Medicine name must be between 2 and 200 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Medicine name can only contain letters, numbers, spaces, and hyphens.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Generic/chemical name of the medicine (e.g., "Paracetamol", "Amoxicillin").
        /// </summary>
        [Required(ErrorMessage = "Generic name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Generic name must be between 2 and 200 characters.")]
        public string GenericName { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description including usage, dosage, and side effects.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Selling price per unit of the medicine.
        /// </summary>
        [Required(ErrorMessage = "Medicine price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99.")]
        public decimal MedicinePrice { get; set; }

        /// <summary>
        /// Minimum stock level before reordering is triggered.
        /// </summary>
        [Required(ErrorMessage = "Reorder level is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder level must be zero or a positive number.")]
        public int ReorderLevel { get; set; }

        /// <summary>
        /// Medicine category classification.
        /// </summary>
        [Required(ErrorMessage = "Category is required.")]
        [EnumDataType(typeof(MedicineCategories), ErrorMessage = "Invalid category value.")]
        public MedicineCategories Category { get; set; }
    }
}