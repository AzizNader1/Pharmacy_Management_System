using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleItemsServices : IApiSaleItemsServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiSaleItemsServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
        {
            _httpClientFactory = httpClientFactory;
            _sessionHelper = sessionHelper;
        }

        private HttpClient CreateAuthenticatedClient()
        {
            var client = _httpClientFactory.CreateClient("PharmacyApi");

            var token = _sessionHelper.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public Task? CreateSaleItemAsync(CreateSaleItemDto saleItem)
        {
            throw new NotImplementedException();
        }

        public Task? DeleteSaleItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesBySaleIdAsync(int saleId)
        {
            throw new NotImplementedException();
        }

        public Task<GetSaleItemDto?> GetSaleItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task? UpdateSaleItemAsync(UpdateSaleItemDto saleItem)
        {
            throw new NotImplementedException();
        }
    }
}
