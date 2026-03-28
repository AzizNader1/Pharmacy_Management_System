using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleServices : IApiSaleServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiSaleServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public Task? CreateSaleAsync(CreateSaleDto sale)
        {
            throw new NotImplementedException();
        }

        public Task? DeleteSaleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto?>> GetAllSalesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<GetSaleDto?> GetSaleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task? UpdateSaleAsync(UpdateSaleDto sale)
        {
            throw new NotImplementedException();
        }

    }
}
