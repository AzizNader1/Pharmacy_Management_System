using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IMedicineRepository
    {
        Task? CreateMedicineAsync(Medicine medicine);
        Task? DeleteMedicineAsync(Medicine medicine);
        Task? UpdateMedicineAsync(Medicine medicine);
        Task<Medicine?> GetMedicineByIdAsync(int id);
        Task<IEnumerable<Medicine?>> GetAllMedicinesAsync();
        Task<Medicine?> GetMedicineByNameAsync(string medicineName);
        Task<IEnumerable<Medicine?>> GetAllMedicinesByCategoryAsync(string categoryName);
    }
}
