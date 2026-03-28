using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiBatchServices : IApiBatchServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiBatchServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public Task? CreateBatchAsync(CreateBatchDto batch)
        {
            throw new NotImplementedException();
        }

        public Task? DeleteBatchAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetBatchDto?>> GetAllBatchesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetBatchDto?> GetBatchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task? UpdateBatchAsync(UpdateBatchDto batch)
        {
            throw new NotImplementedException();
        }
    }
}
