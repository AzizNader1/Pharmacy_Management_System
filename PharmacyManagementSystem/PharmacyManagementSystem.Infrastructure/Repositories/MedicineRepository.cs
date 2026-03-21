using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        public Task<Guid> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteMedicineAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetMedicineDto>> GetAllMedicinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetMedicineDto>> GetAllMedicinesByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<GetMedicineDto> GetMedicineByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetMedicineDto> GetMedicineByNameAsync(string medicineName)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateMedicineAsync(Guid id, UpdateMedicineDto updateMedicineDto)
        {
            throw new NotImplementedException();
        }
    }
}
