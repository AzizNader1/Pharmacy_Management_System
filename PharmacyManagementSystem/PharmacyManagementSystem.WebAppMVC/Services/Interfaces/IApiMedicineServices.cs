using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiMedicineServices
    {
        Task? CreateMedicineAsync(CreateMedicineDto medicine);
        Task? DeleteMedicineAsync(int id);
        Task? UpdateMedicineAsync(UpdateMedicineDto medicine);
        Task<GetMedicineDto?> GetMedicineByIdAsync(int id);
        Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesAsync();
        Task<GetMedicineDto?> GetMedicineByNameAsync(string medicineName);
        Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesByCategoryAsync(string categoryName);
    }
}
