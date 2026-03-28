using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiMedicineServices : IApiMedicineServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiMedicineServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public Task? CreateMedicineAsync(CreateMedicineDto medicine)
        {
            throw new NotImplementedException();
        }

        public Task? DeleteMedicineAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<GetMedicineDto?> GetMedicineByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetMedicineDto?> GetMedicineByNameAsync(string medicineName)
        {
            throw new NotImplementedException();
        }

        public Task? UpdateMedicineAsync(UpdateMedicineDto medicine)
        {
            throw new NotImplementedException();
        }
    }
}
