using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.Application.Interfaces;

namespace PharmacyManagementSystem.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public Task<Guid> CreateSaleAsync(CreateSaleDto createSaleDto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteSaleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto>> GetAllSalesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto>> GetAllSalesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto>> GetAllSalesByUserNameAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetSaleDto> GetSaleByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateSaleAsync(Guid id, UpdateSaleDto updateSaleDto)
        {
            throw new NotImplementedException();
        }
    }
}
