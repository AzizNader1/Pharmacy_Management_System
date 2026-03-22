using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Entities;
using PharmacyManagementSystem.Infrastructure.Data;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task? CreateMedicineAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task? DeleteMedicineAsync(int id)
        {
            _context.Medicines.Remove(_context.Medicines.Find(id)!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Medicine?>> GetAllMedicinesAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<IEnumerable<Medicine?>> GetAllMedicinesByCategoryAsync(string categoryName)
        {
            return await _context.Medicines.Where(m => m.Category.ToString() == categoryName).ToListAsync();
        }

        public async Task<Medicine?> GetMedicineByIdAsync(int id)
        {
            return await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<Medicine?> GetMedicineByNameAsync(string medicineName)
        {
            return await _context.Medicines.FirstOrDefaultAsync(m => m.Name == medicineName);
        }

        public async Task? UpdateMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
        }
    }
}
