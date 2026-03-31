using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;

namespace PharmacyManagementSystem.WebAppMVC.Services.Interfaces
{
    public interface IApiMedicineServices
    {
        Task<GetMedicineDto>? CreateMedicineAsync(CreateMedicineDto medicine);
        Task<bool>? DeleteMedicineAsync(int id);
        Task<GetMedicineDto>? UpdateMedicineAsync(int id, UpdateMedicineDto medicine);
        Task<GetMedicineDto?> GetMedicineByIdAsync(int id);
        Task<List<GetMedicineDto?>> GetAllMedicinesAsync();
        Task<GetMedicineDto?> GetMedicineByNameAsync(string medicineName);
        Task<List<GetMedicineDto?>> GetAllMedicinesByCategoryAsync(string categoryName);
    }
}
