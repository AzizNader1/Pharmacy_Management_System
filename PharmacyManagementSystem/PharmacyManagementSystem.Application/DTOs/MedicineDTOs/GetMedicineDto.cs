using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.DTOs.MedicineDTOs
{
    public class GetMedicineDto
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MedicinePrice { get; set; }
        public int ReorderLevel { get; set; }
        public MedicineCategories Category { get; set; } = MedicineCategories.None;
    }
}
