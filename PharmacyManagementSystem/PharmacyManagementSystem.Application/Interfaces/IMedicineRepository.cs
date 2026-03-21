using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.Application.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Guid> CreateMedicineAsync(CreateMedicineDto createMedicineDto);
        Task<Guid> DeleteMedicineAsync(Guid id);
        Task<Guid> UpdateMedicineAsync(Guid id, UpdateMedicineDto updateMedicineDto);
        Task<GetMedicineDto> GetMedicineByIdAsync(Guid id);
        Task<GetMedicineDto> GetMedicineByNameAsync(string medicineName);
        Task<IEnumerable<GetMedicineDto>> GetAllMedicinesAsync();
        Task<IEnumerable<GetMedicineDto>> GetAllMedicinesByCategoryAsync(string categoryName);
    }
}
