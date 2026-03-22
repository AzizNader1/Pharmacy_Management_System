using PharmacyManagementSystem.Domain.Entities;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IMedicineRepository
    {
        Task? CreateMedicineAsync(Medicine medicine);
        Task? DeleteMedicineAsync(int id);
        Task? UpdateMedicineAsync(Medicine medicine);
        Task<Medicine?> GetMedicineByIdAsync(int id);
        Task<Medicine?> GetMedicineByNameAsync(string medicineName);
        Task<IEnumerable<Medicine?>> GetAllMedicinesAsync();
        Task<IEnumerable<Medicine?>> GetAllMedicinesByCategoryAsync(string categoryName);
    }
}
